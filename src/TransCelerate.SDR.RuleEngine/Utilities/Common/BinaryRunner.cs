using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    /// <summary>
    /// Utility for executing a binary file asynchronously and capturing output
    /// </summary>
    public class BinaryRunner : IBinaryRunner
    {
        /// <summary>
        /// Execute a binary file asynchronously with specified arguments
        /// </summary>
        /// <param name="binaryFile">The full path to the binary file to execute</param>
        /// <param name="args">Command-line arguments to pass to the binary file</param>
        /// <returns>
        /// A <see cref="BinaryResult"/> Result from binary execution
        /// </returns>
        public async Task<BinaryResult> RunAsync(string binaryFile, IEnumerable<string> args)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = binaryFile,
                WorkingDirectory = Path.GetDirectoryName(binaryFile),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            foreach (var arg in args)
            {
                startInfo.ArgumentList.Add(arg);
            }

            var process = new Process { StartInfo = startInfo };

            process.Start();

            var stdOutTask = process.StandardOutput.ReadToEndAsync();
            var stdErrTask = process.StandardError.ReadToEndAsync();

            await Task.WhenAll(stdOutTask, stdErrTask, process.WaitForExitAsync()).ConfigureAwait(false);

            return new BinaryResult(process.ExitCode, stdOutTask.Result, stdErrTask.Result);
        }
    }
}
