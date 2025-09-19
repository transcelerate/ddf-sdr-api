using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    public interface IBinaryRunner
    {
        Task<BinaryResult> RunAsync(string binaryFile, IEnumerable<string> args);
    }
}
