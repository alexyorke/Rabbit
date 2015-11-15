using System;
using NUnit.Framework;

namespace Rabbit.Tests
{
    [TestFixture]
    public class IdParserTests
    {
        [Test]
        public void InvalidRoomIdTest()
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
        public void RoomIdTooLongTest()
        {
            FormatException expectedException = null;

            try
            {
                IdParser.Parse("PWthisroomnameiswaytoolongtoprocessefficientlyomgz12");
            }
            catch (FormatException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
        }

        [Test]
        public void ValidRoomIdTest()
        {
            Assert.AreEqual(IdParser.IsValidStrictRoomId("PWtest12345"), true);
            Assert.AreEqual(IdParser.Parse("PWtest12345"), "PWtest12345");
            Assert.AreEqual(IdParser.Parse("OWopenroom"), "OWopenroom");
            Assert.AreEqual(IdParser.Parse("http://everybodyedits.com/games/PWsomething"), "PWsomething");

        }
    }
}
