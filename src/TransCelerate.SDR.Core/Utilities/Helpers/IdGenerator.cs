using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class IdGenerator
    {
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
