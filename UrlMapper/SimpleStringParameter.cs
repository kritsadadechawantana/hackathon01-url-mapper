using System.Collections.Generic;

namespace UrlMapper
{
    public class SimpleStringParameter : ISimpleStringParameter
    {
        private readonly string pattern;

        public SimpleStringParameter(string pattern)
        {
            this.pattern = pattern;
        }

        public void ExtractVariables(string target, IDictionary<string, string> dicToStoreResults)
        {
            throw new System.NotImplementedException();
        }

        public bool IsMatched(string textToCompare)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetPatterns()
        {
            var result = new string[4];
            result[0] = pattern.Substring(0, 20);
            result[1] = pattern.Substring(21, 8);
            result[2] = pattern.Substring(30,8);
            result[3] = pattern.Substring(39,12);
            return result;
        }
    }
}