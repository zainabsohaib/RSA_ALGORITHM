using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace encryption
{
    class RSACrypto
    {
        public static string GetKeyString(RSAParameters publicKey)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        
        public static string Encrypt(string textToEncrypt)
        {
            RSACryptoServiceProvider cryptoServiceProvider_ = new RSACryptoServiceProvider(2048);
            var publicKey = cryptoServiceProvider_.ExportParameters(false); 
            string publicKeyString = GetKeyString(publicKey);

            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {

                    //rsa.FromXmlString(publicKeyString.ToString());
                    //var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    //var base64Encrypted = Convert.ToBase64String(encryptedData);
                    //return base64Encrypted;
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
