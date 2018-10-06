using System;
using Xunit;

namespace UrlMapper.Tests
{
    public class UnitTest1
    {
        [Fact(Skip = "Goal")]
        public void Test1()
        {
            var strBuilder = new SimpleStringParameterBuilder();
            var strParam = strBuilder.Parse("https://mana.com/app/{app-id}/services/{service-id}");

            var isMatched = strParam.IsMatched("https://mana.com/app/di394/services/878");

            Assert.Equal(true, isMatched); 
        }

        [Fact]
        public void Test_GetPatterns()
        {
            var strParam = new SimpleStringParameter("https://mana.com/app/{app-id}/services/{service-id}");
            var result = strParam.GetPatterns();

            var expected = new string[]{"https://mana.com/app", "{app-id}", "services", "{service-id}"};
            
            Assert.Equal(expected, result);
        }
    }
}
