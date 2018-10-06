using System;
using System.Collections.Generic;
using Xunit;

namespace UrlMapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Matching()
        {
            var strBuilder = new SimpleStringParameterBuilder();
            var strParam = strBuilder.Parse("http://hackathon.com/{username}/none");

            var isMatched = strParam.IsMatched("http://hackathon.com/test 123/none");

            Assert.Equal(true, isMatched);
        }

        [Theory]
        [InlineData("https://mana.com/linkto/{link-id}", new string[] { "https://mana.com/linkto/", "{link-id}" })]
        [InlineData("http://google.com/?s={keyword}&a={hgyyh}", new string[] { "http://google.com/?s=", "{keyword}", "&a=", "{hgyyh}" })]
        [InlineData("https://mana.com/app/{app-id}/services/{service-id}", new string[] { "https://mana.com/app/", "{app-id}", "/services/", "{service-id}" })]
        [InlineData("https://mana.com/app/{app/-id}/services/{service-id}", new string[] { "https://mana.com/app/", "{app/-id}", "/services/", "{service-id}" })]
        public void Test_GetPatterns(string pattern, string[] expected)
        {
            var strParam = new SimpleStringParameter(pattern);
            var result = strParam.GetPatterns(pattern);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("https://mana.com/linkto/A2348", new string[] { "https://mana.com/linkto/", "{link-id}" }, true)]
        [InlineData("http://google.com/?s=value1&a=value2", new string[] { "http://google.com/?s=", "{keyword}", "&a=", "{hgyyh}" }, true)]
        [InlineData("http://google.com/?s=value1&a=", new string[] { "http://google.com/?s=", "{keyword}", "&a=" }, true)]
        [InlineData("https://mana.com/linkto/A2348/services", new string[] { "https://mana.com/linkto/", "{link-id}" ,"services"}, true)]
        public void Test_IsRoutMatch(string url, string[] patternParams, bool expected)
        {
            var strParam = new SimpleStringParameter(string.Empty);
            var result = strParam.IsRoutMatch(url, patternParams);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetValueFromUrl()
        {
            var url = "https://mana.com/linkto/A2348";
            var patternParams = new string[] { "https://mana.com/linkto/", "{link-id}" };
            var expected = new Dictionary<string, string>{{"{link-id}", "A2348"}};
            var strParam = new SimpleStringParameter(string.Empty);
            var result = strParam.GetValueFromUrl(url, patternParams);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetValueFromUrl2()
        {
            var url = "http://hackathon.com/test 123/none";
            var patternParams = new string[] { "http://hackathon.com/", "{username}" ,"/none"};
            var expected = new Dictionary<string, string> { { "{username}", "test 123" } };
            var strParam = new SimpleStringParameter(string.Empty);
            var result = strParam.GetValueFromUrl(url, patternParams);

            Assert.Equal(expected, result);
        }
    }
}
