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
 public class MultiParametersJsonValueProviderFactory : IValueProviderFactory
    {
       public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            IList<ParameterDescriptor> paramtersList = context.ActionContext.ActionDescriptor.Parameters;
            if(paramtersList!=null && paramtersList.Count() > 0)
            {
                foreach (ParameterDescriptor pd in paramtersList)
                {
                    if (pd.BindingInfo != null)
                    {
                        throw new NotSupportedException("Cannot use the MultiParametersJsonvalueProviderFactory when any another BindInfo exists.");
                    }
                }
            }
            return Task.Run(()=> 
            {
                context.ValueProviders.Add(new MultiParametersJsonValueProvider(context));
                
            });
        }
    }
}
