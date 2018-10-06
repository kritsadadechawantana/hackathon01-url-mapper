using System.Collections.Generic;
using System.Linq;
using System;

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
            var isInvalidInput = target == null 
                || dicToStoreResults == null
                || this.pattern == null;
            if(isInvalidInput) return;

            var routParam = GetPatterns(this.pattern);
            var paramValueStore = GetValueFromUrl(target, routParam);
            foreach (var item in paramValueStore)
            {
                dicToStoreResults.Add(item);
            }
        }

        public bool IsMatched(string textToCompare)
        {
            var isInvalidInput = this.pattern == null || textToCompare == null;
            if(isInvalidInput) return false;
            if(this.pattern == textToCompare) return true;

            var routParam = GetPatterns(this.pattern);
            return IsRoutMatch(textToCompare, routParam);
        }

        public string[] GetPatterns(string pattern)
        {
            const string beginParamSymbol = "{";
            const string endParamSymbol = "}";

            var routParams = new List<string>();
            var routPattern = pattern;
            var param = string.Empty;
            while (!string.IsNullOrEmpty(routPattern))
            {
                if (routPattern.StartsWith(beginParamSymbol))
                {
                    var beginIndex = routPattern.IndexOf(beginParamSymbol);
                    var endIndex = routPattern.IndexOf(endParamSymbol);
                    param = routPattern.Substring(beginIndex, endIndex - beginIndex + 1);
                    routParams.Add(param);
                }
                else
                {
                    var beginIndex = 0;
                    var endIndex = routPattern.IndexOf(beginParamSymbol);
                    endIndex = endIndex == -1 ? routPattern.Length : endIndex;
                    param = routPattern.Substring(beginIndex, endIndex - beginIndex);
                    routParams.Add(param);
                }

                routPattern = routPattern.Replace(param, string.Empty);
            }


            return routParams.ToArray();
        }

        private bool isRoutParam(string textToCompare)
        {
            if(textToCompare == null) return false; 
            return textToCompare.StartsWith("{") && textToCompare.EndsWith("}");
        }

        public Dictionary<string, string> GetValueFromUrl(string url, string[] patternParams)
        {
            var targetUrl = url;
            var param = string.Empty;
            var RoutValue = new Dictionary<string, string>();

            for (int index = 0; index < patternParams.Length; index++)
            {
                var currentParam = patternParams[index];
                if (isRoutParam(currentParam))
                {
                    if(!(index == patternParams.Length - 1))
                    {
                        var beginIndex = 0;
                        var endIndex = targetUrl.IndexOf(patternParams[index+1]);
                        endIndex = endIndex == -1 ? targetUrl.Length : endIndex;
                        param = targetUrl.Substring(beginIndex, endIndex);
                    }else
                    {
                        var beginIndex = 0;
                        var endIndex = targetUrl.Length;
                        param = targetUrl.Substring(beginIndex, endIndex);
                    }
                    RoutValue.Add(currentParam, param);
                }
                else
                {
                    if (!targetUrl.StartsWith(currentParam)) return RoutValue;
                    param = currentParam;
                }

                if (!string.IsNullOrEmpty(param)) targetUrl = targetUrl.Replace(param, string.Empty);
            }

            return RoutValue;
        }

        public bool IsRoutMatch(string url, string[] patternParams)
        {
            var targetUrl = url;
            var param = string.Empty;

            for (int index = 0; index < patternParams.Length; index++)
            {
                var currentParam = patternParams[index];
                if (isRoutParam(currentParam))
                {
                    if(!(index == patternParams.Length - 1))
                    {
                        var beginIndex = 0;
                        var endIndex = targetUrl.IndexOf(patternParams[index + 1]);
                        endIndex = endIndex == -1 ? targetUrl.Length : endIndex;
                        param = targetUrl.Substring(beginIndex, endIndex);
                    }
                    else
                    {
                        var beginIndex = 0;
                        var endIndex = targetUrl.Length;
                        param = targetUrl.Substring(beginIndex, endIndex);
                    }
                }
                else
                {
                    if (!targetUrl.StartsWith(currentParam)) return false;
                    param = currentParam;
                }

                if (!string.IsNullOrEmpty(param)) targetUrl = targetUrl.Replace(param, string.Empty);
                
            }

            if (!string.IsNullOrEmpty(targetUrl)) return false;

            return true;
        }
    }
}