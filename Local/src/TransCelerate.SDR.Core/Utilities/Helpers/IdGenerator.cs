using System;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class IdGenerator
    {
        /// <summary>
        /// Used for generating UUID
        /// </summary>
        /// <returns>
        /// A new <see cref="Guid"/>         
        /// </returns>
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
