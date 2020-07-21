using Swashbuckle.Application;
using System.Web.Http;


namespace ShippingApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //GlobalConfiguration.Configuration
            //  .EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
            //  .EnableSwaggerUi();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
