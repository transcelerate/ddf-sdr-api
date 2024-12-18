namespace TransCelerate.SDR.Core.ErrorModels
{
    /// <summary>
    /// This class is a Model for validation errors
    /// </summary>
    public class ValidationErrorModel
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
    }
}
