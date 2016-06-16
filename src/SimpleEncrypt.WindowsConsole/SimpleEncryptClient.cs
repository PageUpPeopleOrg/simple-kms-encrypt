using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace SimpleEncrypt.WindowsConsole
{
    public static class SimpleEncryptClient
    {
        public static async Task<string> DecryptAsync(this string encryptedValue, string regionName)
        {
            var client = new AmazonKeyManagementServiceClient(RegionEndpoint.GetBySystemName(regionName));

            var ciphertestStream = new MemoryStream(Convert.FromBase64String(encryptedValue)) { Position = 0 };

            var decryptRequest = new DecryptRequest { CiphertextBlob = ciphertestStream };

            var response = await client.DecryptAsync(decryptRequest);

            var buffer = new byte[response.Plaintext.Length];

            var bytesRead = response.Plaintext.Read(buffer, 0, (int)response.Plaintext.Length);

            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public static async Task<string> EncryptAsync(this string value, string key, string regionName)
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

            var response = await client.EncryptAsync(encryptRequest);

            var buffer = new byte[response.CiphertextBlob.Length];

            response.CiphertextBlob.Read(buffer, 0, (int)response.CiphertextBlob.Length);

            return Convert.ToBase64String(buffer);
        }
    }
}
