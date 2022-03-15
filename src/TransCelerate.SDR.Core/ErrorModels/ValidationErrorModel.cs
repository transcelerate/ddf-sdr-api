namespace TransCelerate.SDR.Core.ErrorModels
{
    /// <summary>
    /// This class is a Model for validation errors
    /// </summary>
    public class ValidationErrorModel
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public object error { get; set; }
    }
}
