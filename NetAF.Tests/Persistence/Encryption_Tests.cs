using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Persistence;

namespace NetAF.Tests.Persistence
{
    [TestClass]
    public class Encryption_Tests
    {
        [TestMethod]
        public void GivenTestString_WhenEncrypt_ThenCorrectlyExcryptedStringReturned()
        {
            var value = "Test";
            var expected = "hNDu9VF0mlFz45aM8SkTKw==";

            var result = Encryption.Encrypt(value);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GivenExcryptedTestString_WhenDecrypt_ThenCorrectlyDecryptedStringReturned()
        {
            var expected = "Test";
            var value = "hNDu9VF0mlFz45aM8SkTKw==";

            var result = Encryption.Decrypt(value);

            Assert.AreEqual(expected, result);
        }
    }
}
