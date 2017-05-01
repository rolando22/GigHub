using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Practices.Unity.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using GigHub.App_Start;

namespace GigHub.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // CORS Config
            // TODO: Add domain lists separated with commas
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // IoC Container
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            var settings = config.Formatters.JsonFormatter.SerializerSettings;

            // Clear XML as Supported Media Type
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // CamelCase and Indented
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}