namespace TransCelerate.SDR.Core.ErrorModels
{
    /// <summary>
    /// This class is a model for rule validation errors
    /// </summary>
    public class RuleValidationErrorModel
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public object Warning { get; set; }
    }
}
