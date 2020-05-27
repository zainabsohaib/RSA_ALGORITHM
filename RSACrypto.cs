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

        public static string Decrypt(string textToDecrypt)
        {
            // RSACryptoServiceProvider cryptoServiceProvider_ = new RSACryptoServiceProvider(2048);
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048);

            var privateKey = cryptoServiceProvider.ExportParameters(true);
            var publicKey = cryptoServiceProvider.ExportParameters(false);

            string privateKeyString = GetKeyString(privateKey);
    

            var bytesToDescrypt = Encoding.UTF8.GetBytes(textToDecrypt);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(privateKeyString);

                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
