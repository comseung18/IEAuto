using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace IEAuto
{
    class Secret
    {
        SHA256 sha;
        public Secret()
        {
            sha = new SHA256Managed();
        }

        public string SHA256Hash(string data)
        {
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach(byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        // 암호화 키 생성
        public static Rfc2898DeriveBytes CreateKey(string passwd)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(passwd);
            byte[] saltBytes = SHA512.Create().ComputeHash(keyBytes);

            Rfc2898DeriveBytes result = new Rfc2898DeriveBytes(keyBytes, saltBytes, 100000);
            return result;
        }

        
        public static byte[] AesEncrypt(byte[] origin, string passwd)
        {
            RijndaelManaged aes = new RijndaelManaged();
            Rfc2898DeriveBytes key = CreateKey(passwd);
            Rfc2898DeriveBytes vector = CreateKey(passwd);

            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key.GetBytes(32);
            aes.IV = vector.GetBytes(16);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using(MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(origin, 0, origin.Length);
                }
                return ms.ToArray();
            }
        }

        public static byte[] AesDecrypt(byte[] origin, string password)
        {
            RijndaelManaged aes = new RijndaelManaged();   
            Rfc2898DeriveBytes key = CreateKey(password);    
            Rfc2898DeriveBytes vector = CreateKey(password);   

            aes.BlockSize = 128;          
            aes.KeySize = 256;           
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key.GetBytes(32);   
            aes.IV = vector.GetBytes(16);  

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream()) 
            {
                
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(origin, 0, origin.Length);
                }
                return ms.ToArray();
            }
        }
    }
}
