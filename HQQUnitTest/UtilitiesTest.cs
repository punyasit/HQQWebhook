using Microsoft.VisualStudio.TestTools.UnitTesting;
using HQQLibrary.Utilities;

namespace HQQUnitTest
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void U01_TestHMAC()
        {
            string result = string.Empty;
            result = Utilities.HashHmac(Utilities.HMACCoding.SHA256,
                "EAAaWObAR2tIBAAKt7bR4K87Wlq4ck1djgfRuZBDbyinquvWkT5oPfa5sZCjCWUHGcr3tojDqTHC9eLhgfwRQOBdX4pKAmZCrtGnShSa7FOPaJCZBjLN4MYyvqVdbnkvC1Uiok1kMwBnZAvxeQiAYZCzYFAHee4yfU3O0SkHe9jFNwPczoMZC8IeRhU6MozayjcZD",
                "8d48345f13d95d6241ec511fbf04d1cd");

            Assert.IsNotNull(result);
        }
    }
}
