using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Rabbit.Rabbit.GetAuthType("somethingemail@example.com","apasswordorsomething"), Rabbit.Auth.AuthType.Regular);
        }
    }
}
