﻿using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassOne.Domain;

namespace PassOneUnitTests.DomainTests
{
    [TestClass]
    public class CredentialsSoapTests : PassOneSoapTests
    {
        private readonly PassOneCredentials _encryptedTestPassOneCredentials = new PassOneCredentials(1, 1, 
                                                                                 new byte[]
                                                                                     {
                                                                                         194, 223, 221, 129, 93, 130, 194,
                                                                                         247, 26, 65, 188, 94, 235, 1,
                                                                                         107, 145, 16, 234, 75, 158, 107
                                                                                         , 206, 252, 17, 173, 108, 28,
                                                                                         37, 74, 185, 23, 246
                                                                                     },
                                                                                 new byte[]
                                                                                     {
                                                                                         184, 120, 133, 72, 132, 43, 194,
                                                                                         189, 127, 93, 248, 47, 24, 183,
                                                                                         47, 87, 113, 54, 89, 107, 82,
                                                                                         246, 190, 54, 179, 199, 17, 218
                                                                                         , 208, 114, 87, 179
                                                                                     },
                                                                                 new byte[]
                                                                                     {
                                                                                         37, 31, 120, 109, 1, 164, 173, 116
                                                                                         , 219, 239, 41, 42, 142, 30,
                                                                                         199, 107
                                                                                     },
                                                                                 new byte[]
                                                                                     {
                                                                                         83, 80, 207, 238, 210, 116, 207,
                                                                                         22, 121, 54, 25, 47, 179, 51,
                                                                                         32, 153
                                                                                     },
                                                                                 new byte[]
                                                                                     {
                                                                                         7, 60, 8, 228, 65, 103, 83, 207,
                                                                                         176, 219, 171, 5, 235, 51, 66,
                                                                                         108, 171, 254, 9, 114, 74, 38,
                                                                                         10, 226, 111, 129, 141, 229,
                                                                                         247, 1, 203, 64
                                                                                     });
        //Constructor
        public CredentialsSoapTests()
        {
            TestPassOneUser = new PassOneUser(TestPassOneUser.Id, TestPassOneUser.FirstName, TestPassOneUser.LastName, TestPassOneUser.Username, TestPassOneUser.Password,
                                new byte[]
                                    {
                                        220, 1, 103, 95, 8, 241, 44, 75, 252, 211, 167, 232, 169, 41, 181, 122, 51, 118,
                                        66, 175, 96
                                        ,
                                        102, 163, 243, 26, 232, 40, 189, 174, 181, 229, 13
                                    },
                                new byte[] {229, 219, 79, 110, 4, 98, 121, 23, 194, 214, 43, 142, 22, 247, 128, 206});
        }

        /// <summary>
        /// Test for encryption only, should pass.
        /// </summary>
        [TestMethod]
        public void TestEncrypt_Pass()
        {
            var test = TestPassOneCredentials;
            test.Encrypt(TestPassOneUser.Encryption);

            Assert.AreEqual(test, _encryptedTestPassOneCredentials);
        }

        /// <summary>
        /// Test for decryption only, should pass.
        /// </summary>
        [TestMethod]
        public void TestDecrypt_Pass()
        {
            var test = _encryptedTestPassOneCredentials;

            test.Decrypt(TestPassOneUser.Encryption);
            Assert.AreEqual(test, TestPassOneCredentials);
        }

        /// <summary>
        /// Test should throw a CryptographicException.
        /// </summary>
        [TestMethod]
        public void TestDecrypt_Fail_WrongEncryptionKey()
        {
            try
            {
                var test = _encryptedTestPassOneCredentials;
                test.Decrypt(TestUser2.Encryption);
                Assert.Fail();
            }
            catch (CryptographicException)
            {
            }
        }

        /// <summary>
        /// Testing that the encryption methods work together to acheive two-way data encryption, should pass. 
        /// </summary>
        [TestMethod]
        public void TestEncryptDecryptPass()
        {
            var test = TestPassOneCredentials;
            test.Encrypt(TestPassOneUser.Encryption);

            Assert.AreEqual(test, TestPassOneCredentials);

            TestPassOneCredentials.Decrypt(TestPassOneUser.Encryption);
            Assert.AreEqual(test, TestPassOneCredentials);
        }

        /// <summary>
        /// Test should fail since the expected value is different than the decrypted value.
        /// </summary>
        [TestMethod]
        public void TestEncryptDecryptFail()
        {
            var test = TestPassOneCredentials;
            test.Encrypt(TestPassOneUser.Encryption);

            Assert.AreEqual(test, _encryptedTestPassOneCredentials);

            test.Decrypt(TestPassOneUser.Encryption);
            Assert.AreNotEqual(test, TestCredentials2);
        }

    }
}
