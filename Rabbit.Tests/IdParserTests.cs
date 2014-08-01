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
            Assert.AreEqual(IdParser.IsValidStrictRoomId("PWtest12345"), true);
            Assert.AreEqual(IdParser.Parse("PWtest12345"), "PWtest12345");
        }
    }
}
