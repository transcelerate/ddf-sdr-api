namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    /// <summary>
    /// Result from an external binary execution
    /// </summary>
    public class BinaryResult
    {
        public int ExitCode { get; }
        public string StdOut { get; }
        public string StdErr { get; }

        public BinaryResult(int exitCode, string stdOut, string stdErr)
        {
            ExitCode = exitCode;
            StdOut = stdOut;
            StdErr = stdErr;
        }
    }
}
