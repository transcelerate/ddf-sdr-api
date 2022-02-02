using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.ErrorModels
{
    public class ValidationErrorModel
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public object error { get; set; }
    }
}
