using System.IO;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    /// <summary>
    /// Common file system operations that delegates directly to <see cref="System.IO.File"/>
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public bool Exists(string path) => File.Exists(path);
        public Task WriteAllTextAsync(string path, string content) => File.WriteAllTextAsync(path, content);
        public Task<string> ReadAllTextAsync(string path) => File.ReadAllTextAsync(path);
    }
}
