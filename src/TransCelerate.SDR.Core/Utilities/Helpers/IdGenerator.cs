using System;
using System.Collections.Generic;
using System.Text;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class IdGenerator
    {
        /// <summary>
        /// Used for generating UUID
        /// </summary>
        /// <returns></returns>
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
