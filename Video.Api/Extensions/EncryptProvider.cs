using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Video.Api.Extensions
{
    public class EncryptProvider
    {
        /// <summary>
        /// Create RSAParameters
        /// </summary>
        /// <returns></returns>
        public static RSAParameters GenerateParameters()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}
