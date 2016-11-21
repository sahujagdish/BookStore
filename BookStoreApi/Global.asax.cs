using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using BookStore.Infrastructure;
using Newtonsoft.Json;

namespace BookStoreApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Bootstrapper.Initialise();

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            // settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var appXmlType = formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            //Add CORS Handler
           // GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());

        }
    }
}
