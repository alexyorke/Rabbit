using System;
using NUnit.Framework;

namespace Rabbit.Tests
{
    [TestFixture]
    public class IdParserTests
    {
        [Test]
        public void InvalidRoomIDTest()
        {
            FormatException expectedException = null;

            try
            {
                IdParser.Parse("test");
            }

            catch (FormatException ex)
            {
                expectedException = ex;
            }

            Assert.AreEqual(IdParser.IsValidStrictRoomId("test"), false);
            Assert.IsNotNull(expectedException);
        }

        [Test]
        public void ValidRoomIDTest()
        {
            Assert.AreEqual(IdParser.IsValidStrictRoomId("PWmSQ3dUeya0I"), true);
            Assert.AreEqual(IdParser.Parse("PWmSQ3dUeya0I"), "PWmSQ3dUeya0I");
        }
    }
}
