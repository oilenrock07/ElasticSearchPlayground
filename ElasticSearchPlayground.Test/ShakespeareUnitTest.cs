using ElasticSearchPlayground.Repository;
using ElasticSearchPlayground.Repository.Models;
using Nest;
using NUnit.Framework;

namespace ElasticSearchPlayground.Test
{
    public class ShakespeareUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var shakespeareRepository = new ShakespeareRepository();
            var filter = new ElasticSearchFilter();
            var result = shakespeareRepository.Search(filter).GetAwaiter().GetResult();

            

            Assert.Pass();
        }
    }
}