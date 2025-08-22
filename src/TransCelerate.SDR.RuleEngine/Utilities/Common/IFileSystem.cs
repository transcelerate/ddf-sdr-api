using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    public interface IFileSystem
    {
        bool Exists(string path);
        Task WriteAllTextAsync(string path, string content);
        Task<string> ReadAllTextAsync(string path);
    }
}
