namespace TransCelerate.SDR.Core.Utilities
{
    public interface ILogHelper
    {
        void LogCriitical(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogInformation(string message);
        void LogTrace(string message);
        void LogWarning(string message);
    }
}