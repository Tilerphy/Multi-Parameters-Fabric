using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace MultiParametersWebApi
{
public class MultiParametersJsonValueProvider : IValueProvider
    {
        private Dictionary<string, object> p = new Dictionary<string, object>();

        public MultiParametersJsonValueProvider(ValueProviderFactoryContext context)
        {
            using (StreamReader reader = new StreamReader(context.ActionContext.HttpContext.Request.Body))
            {
                string debug = reader.ReadToEnd();
                JsonTextReader s = new JsonTextReader(new StringReader(debug.Trim()));
                while (s.Read())
                {   
                    if (p.ContainsKey(s.Path))
                    {
                        p[s.Path] = s.Value;
                    }
                    else
                    {
                        p.Add(s.Path, s.Value);
                    }

                }
            }
        }
        public bool ContainsPrefix(string prefix)
        {
            return p.ContainsKey(prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            if (p.ContainsKey(key))
            {
                ValueProviderResult s = new ValueProviderResult(new StringValues(p[key] == null ? "" : p[key].ToString()));
                return s;
            }
            else
            {
                ValueProviderResult s = new ValueProviderResult(new StringValues(string.Empty));
                return s;
            }
            
        }
    }
}
