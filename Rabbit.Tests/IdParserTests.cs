using System;
using NUnit.Framework;

namespace Rabbit.Tests
{
    [TestFixture]
    public class IdParserTests
    {

        [Test]
        public void RoomIdTooLongTest()
        {
            Exception expectedException = null;

            try
            {
                string worldid;
                IdParser.TryParse("http://domain.com/PWthisroomnameiswaytoolongtoprocessefficientlyomgz12", out worldid);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            Assert.IsNull(expectedException);
        }
    }
}
