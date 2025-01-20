using System.Security.Cryptography;
using System.Text;

namespace ViisApp.Lib
{
    public class CryptoHelper
    {
        public static byte[] GenerateFixedAesKey(int keySize)
        {
            // 创建一个具有固定种子的Random实例，以保证每次生成的密钥相同
            Random random = new Random(20240714); // 使用固定的种子

            // 创建一个指定长度的字节数组作为密钥
            byte[] key = new byte[keySize / 8];
            random.NextBytes(key); // 使用Random填充密钥数组

            return key;
        }

        public static byte[] GenerateIV()
        {
            // 创建一个随机数生成器
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] iv = new byte[16]; // AES块大小为16字节
                rng.GetBytes(iv); // 填充IV
                return iv;
            }


        }

        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateFixedAesKey(256);
                aesAlg.IV = GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                        csEncrypt.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray()) + "|" + Convert.ToBase64String(aesAlg.IV);
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            //TODO: Implement decryption logic here
            var arr = encryptedText.Split('|');
            byte[] encryptedBytes = Convert.FromBase64String(arr[0]);
            string iv = arr[1];
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateFixedAesKey(256);
                aesAlg.IV = Convert.FromBase64String(iv);
                aesAlg.Mode = CipherMode.CBC; // 确保使用加密时相同的模式
                aesAlg.Padding = PaddingMode.PKCS7; // 设置填充模式

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // 使用StreamReader读取解密后的数据，它会自动处理填充
                            string decryptedString = srDecrypt.ReadToEnd();
                            return decryptedString;
                        }
                    }
                }
            }
        }

        public static bool VerifyData(string plainText)
        {
            var args = plainText.Split("|");
            // 使用RSA算法验证签名
            var originalData = Encoding.UTF8.GetBytes(args[0]);
            var signatureBytes = Convert.FromBase64String(args[1]);
            var publicKey = args[2];
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                return rsa.VerifyData(originalData, "SHA256", signatureBytes);
            }
        }

        public static string SignData(string plainText)
        {
            // 使用RSA算法生成签名
            var originalData = Encoding.UTF8.GetBytes(plainText);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(GetPrivateKey());
                var signatureBytes = rsa.SignData(originalData, "SHA256");
                return Convert.ToBase64String(signatureBytes);
            }
        }

        public static string GetPublicKey()
        {
            var publicKey = File.ReadAllText("MyKeyPair.pub");
            return publicKey;
        }

        public static string GetPrivateKey()
        {
            var privateKey = File.ReadAllText("MyKeyPair.prv");
            return privateKey;
        }

        public static void GenerateKeyPair()
        {
            // 生成RSA密钥对
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                // 保存私钥
                var privateKey = rsa.ToXmlString(true);
                // 保存公钥
                var publicKey = rsa.ToXmlString(false);

                // 保存密钥对
                SaveKeyPair("MyKeyPair", privateKey, publicKey);
            }
        }

        public static void SaveKeyPair(string keyName, string privateKey, string publicKey)
        {
            //TODO: Implement saving logic here
            File.WriteAllText(keyName + ".prv", privateKey);
            File.WriteAllText(keyName + ".pub", publicKey);
        }


    }
}
