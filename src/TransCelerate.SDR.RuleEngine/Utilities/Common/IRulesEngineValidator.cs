using System.Threading.Tasks;

namespace TransCelerate.SDR.RuleEngine.Utilities.Common
{
    public interface IRulesEngineValidator
    {
        Task<BinaryResult> ValidateAsync(string json);
    }
}
