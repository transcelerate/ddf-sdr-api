using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    public static class BinaryRunner
    {
        public static async Task<(int ExitCode, string StdOut, string StdErr)> RunAsync(string binaryFile, string args)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = binaryFile,
                Arguments = args,
                WorkingDirectory = Path.GetDirectoryName(binaryFile),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = startInfo };

            process.Start();

            var stdOutTask = process.StandardOutput.ReadToEndAsync();
            var stdErrTask = process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            return (process.ExitCode, await stdOutTask, await stdErrTask);
        }
    }
}
