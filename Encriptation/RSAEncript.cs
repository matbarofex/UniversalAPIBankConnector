using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Encriptation {
    internal static class RSAEncript {

        public static bool IsKeySizeValid(int keySize) {
            return keySize >= 384 &&
                    keySize <= 16384 &&
                    keySize % 8 == 0;
        }

        public static string EncryptText(string text, int keySize, string publicKeyXml) {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
            return Convert.ToBase64String(encrypted);
        }

        public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml) {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            int maxLength = GetMaxDataLength(keySize);
            if (data.Length > maxLength) throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize)) {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }

        private static bool _optimalAsymmetricEncryptionPadding = false;

        public static int GetMaxDataLength(int keySize) {
            if (_optimalAsymmetricEncryptionPadding) {
                return ((keySize - 384) / 8) + 7;
            }
            return ((keySize - 384) / 8) + 37;
        }

    }
}
