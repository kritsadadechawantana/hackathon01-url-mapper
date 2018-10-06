using System.Collections.Generic;
using System.Linq;

namespace UrlMapper
{
    public class SimpleStringParameter : ISimpleStringParameter
    {
        private string pattern;

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
             var routParams = new List<string>();
             var routPattern = this.pattern;
             var param = string.Empty;
            while (!string.IsNullOrEmpty(routPattern))
            {
                if (routPattern.StartsWith("{"))
                {
                    var beginIndex = routPattern.IndexOf("{");
                    var endIndex = routPattern.IndexOf("}");
                    param = routPattern.Substring(beginIndex, endIndex - beginIndex);
                    routParams.Add(param);
                }
                else{
                    var beginIndex = 0;
                    var endIndex = routPattern.IndexOf("{");
                    param = routPattern.Substring(beginIndex, endIndex - beginIndex - 1);
                    routParams.Add(param);
                }

                routPattern.Replace($"{param}/", string.Empty);
            }
            
            
            return routParams.ToArray();
        }
    }
}