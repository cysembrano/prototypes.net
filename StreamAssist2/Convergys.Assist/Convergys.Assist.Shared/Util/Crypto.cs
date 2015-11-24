using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Logging;

namespace Convergys.Assist.Shared.Util
{

    public sealed class Crypto
    {
        private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private string ENCRYPTION_KEY = "?$^#~&*;";

        private static Crypto instance;
        private Crypto() { }
        private static Crypto Instance
        {
            get
            {
                if (instance == null)
                    instance = new Crypto();
                return instance;
            }
        }

        private string EncryptDES(string clearText)
        {
            var strReturn = string.Empty;
            try
            {
                var key = System.Text.Encoding.UTF8.GetBytes(ENCRYPTION_KEY);
                var provider = new DESCryptoServiceProvider();
                var input = Encoding.UTF8.GetBytes(clearText);

                var memStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memStream, provider.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();
                strReturn = Convert.ToBase64String(memStream.ToArray());
            }
            catch (Exception ex)
            {
                Log4NetManager.Instance.ErrorFormat(this.GetType(), "Exception {0} \n Inner Exception {1}", ex.ToString(), ex.InnerException.ToString());
                throw;
            }

            return strReturn;
        }

        private string DecryptDES(string clearText)
        {
            var strReturn = string.Empty;
            try
            {
                byte[] input = new byte[clearText.Length];
                var key = System.Text.Encoding.UTF8.GetBytes(ENCRYPTION_KEY);
                var provider = new DESCryptoServiceProvider();
                input = Convert.FromBase64String(clearText);

                var memStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memStream, provider.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();

                var encoding = System.Text.Encoding.UTF8;

                strReturn = encoding.GetString(memStream.ToArray());
            }
            catch (Exception ex)
            {
                Log4NetManager.Instance.ErrorFormat(this.GetType(), "Exception {0} \n Inner Exception {1}", ex.ToString(), ex.InnerException.ToString());
                throw;
            }

            return strReturn;
        }

        private string CreatePassword(int length)
        {
            const string valid = "23456789abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVXXYZ";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string Encrypt(string clearText)
        {
            return Crypto.Instance.EncryptDES(clearText);
        }

        public static string Decrypt(string clearText)
        {
            return Crypto.Instance.DecryptDES(clearText);
        }

        public static string GeneratePassword(int charLength)
        {
            return Crypto.Instance.CreatePassword(charLength);
        }
        
       
    }
}
