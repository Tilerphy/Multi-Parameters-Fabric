using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Newtonsoft.Json.Linq;

namespace MultiParametersWebApi
{
public class MultiParametersJsonValueProvider : IValueProvider
    {
        private JContainer _container;
        public MultiParametersJsonValueProvider(ValueProviderFactoryContext context)
        {
            using (StreamReader reader = new StreamReader(context.ActionContext.HttpContext.Request.Body))
            {
                string debug = reader.ReadToEnd().Trim();

                _container = debug.StartsWith("[", StringComparison.OrdinalIgnoreCase) ?
                    (JContainer)JArray.Parse(debug) :
                    (JContainer)JObject.Parse(debug);
            }
        }
        public bool ContainsPrefix(string prefix)
        {
            JToken token = _container.SelectToken(prefix);
            return token != null;
        }
        public ValueProviderResult GetValue(string key)
        {
            JToken token = _container.SelectToken(key);
            if (token == null)
            {
                ValueProviderResult s = new ValueProviderResult(new StringValues(string.Empty));
                return s;
            }
            else 
            {
                ValueProviderResult s = new ValueProviderResult(new StringValues(token.Value<string>()));
                return s;
            }
        }
    }
}
