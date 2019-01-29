using GameStore.Web.Mapping;
using NUnit.Framework;

namespace GameStore.Tests
{
    [TestFixture]
    public class AutoMapperTest
    {
        [Test]
        public void MapperTest()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
