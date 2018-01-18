using System.Web.Http;
using APM.WebApi.App_Start;
using APM.WebApi.Models.Interfaces;
using APM.WebAPI.Models;
using Newtonsoft.Json.Serialization;
using Unity;
using Unity.Lifetime;

namespace APM.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			var container = new UnityContainer();
			container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);
		}
    }
}
