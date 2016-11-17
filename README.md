# Multi-Parameters-Fabric

Support Multi-Parameters in WebApi of Microsoft.Fabric. 


<h2>Usage:</h2>


 services.AddMvc(options =\>
            {

               //...
                options.ValueProviderFactories.Add(new MultiParametersJsonValueProviderFactory());
                //...
            });
