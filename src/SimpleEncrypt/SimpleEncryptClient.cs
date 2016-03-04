using System;
using System.IO;
using System.Text;
using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace SimpleEncrypt
{
    public static class SimpleEncryptClient
    {
        public static string Decrypt(this string encryptedValue, string regionName)
        {
            var client = new AmazonKeyManagementServiceClient(RegionEndpoint.GetBySystemName(regionName));

            var ciphertestStream = new MemoryStream(Convert.FromBase64String(encryptedValue)) { Position = 0 };

            var decryptRequest = new DecryptRequest { CiphertextBlob = ciphertestStream };

            var response = client.Decrypt(decryptRequest);

            var buffer = new byte[response.ContentLength];

            response.Plaintext.Read(buffer, 0, (int)response.ContentLength);

            return Encoding.UTF8.GetString(buffer);
        }

        public static string Encrypt(this string value, string key, string regionName)
        {
            var client = new AmazonKeyManagementServiceClient(RegionEndpoint.GetBySystemName(regionName));

            var plaintextData = new MemoryStream(Encoding.UTF8.GetBytes(value))
            {
                Position = 0
            };

            var encryptRequest = new EncryptRequest
            {
                KeyId = key,
                Plaintext = plaintextData
            };

            var response = client.Encrypt(encryptRequest);

            var buffer = new byte[response.CiphertextBlob.Length];

            response.CiphertextBlob.Read(buffer, 0, (int)response.CiphertextBlob.Length);

            return Convert.ToBase64String(buffer);
        }
    }
}
