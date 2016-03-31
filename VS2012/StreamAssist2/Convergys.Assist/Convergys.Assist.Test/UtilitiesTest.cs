using System;
using Convergys.Assist.Shared.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convergys.Assist.Test
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void Crypto_EncryptDecryptDES()
        {
            var encrypted = Crypto.Encrypt("TestMe");
            var decrypted = Crypto.Decrypt(encrypted);

            Assert.AreEqual("TestMe", decrypted);
        }

        [TestMethod]
        public void Crypto_DecryptEncryptDES()
        {
            string pwd = "5mQmSnQf";
            string encrpwd = "q6J2u5NBNqez2YMC6v6U8A==";
            var decrypted = Crypto.Decrypt(encrpwd);
            var encrypted = Crypto.Encrypt(pwd);


            Assert.AreEqual(encrpwd, encrypted);
        }
    }
}
