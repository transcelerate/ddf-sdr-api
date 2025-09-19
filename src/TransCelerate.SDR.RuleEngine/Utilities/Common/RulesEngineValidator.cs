using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    /// <summary>
    /// Utility for executing CDISC Rules Engine Validate
    /// </summary>
    public class RulesEngineValidator : IRulesEngineValidator
    {
        private readonly string _binaryFile = Path.Combine(Config.CdiscRulesEngine, Config.CdiscRulesEngineRelativeBinary);
        private readonly string[] _args;
        private readonly string _cachePath = Path.Combine(Config.CdiscRulesEngine, Config.CdiscRulesEngineRelativeCache);

        private readonly string _tempInput;
        private readonly string _tempOutput;
        private readonly string _reportFile;

        private readonly IBinaryRunner _binaryRunner;
        private readonly IFileSystem _fileSystem;

        public RulesEngineValidator(IBinaryRunner binaryRunner, IFileSystem fileSystem)
        {
            _binaryRunner = binaryRunner;
            _fileSystem = fileSystem;

            _tempInput = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

            // Output file name does not expect file extension
            _tempOutput = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}");
            _reportFile = $"{_tempOutput}.json";

            _args = ["validate", "-s", "usdm", "-v", "4-0", "-dp", _tempInput, "-o", _tempOutput, "-of", "json"];

            if (!string.IsNullOrEmpty(_cachePath) && Directory.Exists(_cachePath))
            {
                _args.Append("-ca");
                _args.Append(_cachePath);
            }
        }

        public async Task<BinaryResult> ValidateAsync(string json)
        {
            await _fileSystem.WriteAllTextAsync(_tempInput, json);

            var binaryResult = await _binaryRunner.RunAsync(_binaryFile, _args);

            if (binaryResult.ExitCode != 0)
            {
                // Rules Engine failed
                return new BinaryResult
                    (
                        binaryResult.ExitCode,
                        string.Empty,
                        binaryResult.StdErr
                    );
            }

            if (!_fileSystem.Exists(_reportFile))
            {
                // Report not found
                return new BinaryResult
                    (
                        0,
                        string.Empty,
                        Constants.ErrorMessages.ErrorMessageForCdiscRulesEngineOutputNotFound
                    );
            }

            var report = await _fileSystem.ReadAllTextAsync(_reportFile);

            if (HasFullyExecutableIssues(report))
            {
                // Validate found issues
                return new BinaryResult
                    (
                        0,
                        report,
                        Constants.ErrorMessages.ErrorMessageForCdiscRulesEngineIssuesFound
                    );
            }

            try { File.Delete(_tempInput); } catch { }
            try { File.Delete(_reportFile); } catch { }

            return new BinaryResult(0, report, string.Empty);
        }

        // Has Issue_Details "executability": "fully executable" = Error
        private static bool HasFullyExecutableIssues(string report)
        {
            var reportJson = JObject.Parse(report);
            if (reportJson.TryGetValue("Issue_Details", out JToken token) && token is JArray issueDetailsArr)
            {
                foreach (var issue in issueDetailsArr)
                {
                    JToken execToken = issue["executability"];
                    // If executability property doesn't exist or is null, consider it fully executable
                    // Otherwise, check if it contains "fully executable"
                    bool isFullyExecutable = execToken == null
                        || execToken.Type == JTokenType.Null
                        || execToken.ToString().Contains("fully executable", StringComparison.OrdinalIgnoreCase);

                    if (isFullyExecutable)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
