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

        [Theory]
        [InlineData("https://mana.com/linkto/{link-id}", new string[] { "https://mana.com/linkto/", "{link-id}" })]
        [InlineData("http://google.com/?s={keyword}&a={hgyyh}", new string[] { "http://google.com/?s=", "{keyword}", "&a=", "{hgyyh}" })]
        [InlineData("https://mana.com/app/{app-id}/services/{service-id}", new string[]{"https://mana.com/app/", "{app-id}", "/services/", "{service-id}"})]
        [InlineData("https://mana.com/app/{app/-id}/services/{service-id}", new string[]{"https://mana.com/app/", "{app/-id}", "/services/", "{service-id}"})]
        public void Test_GetPatterns(string pattern, string[] expected)
        {
            var strParam = new SimpleStringParameter(pattern);
            var result = strParam.GetPatterns();

            Assert.Equal(expected, result);
        }
    }
}
