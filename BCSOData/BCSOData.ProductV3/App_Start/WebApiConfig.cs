using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using BCSOData.ProductV3.Models;
using System.Web.Http.OData.Formatter;

namespace BCSOData.ProductV3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //var odataFormatters = ODataMediaTypeFormatters.Create();
            //odataFormatters = odataFormatters.Where(
            //    f => f.SupportedMediaTypes.Any(
            //        m => m.MediaType == "application/atom+xml" ||
            //            m.MediaType == "application/atomsvc+xml")).ToList();

            //config.Formatters.Clear();
            //config.Formatters.AddRange(odataFormatters);

            config.SuppressHostPrincipal();

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            //builder.DataServiceVersion = new Version("2.0");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            
            
        }
    }
}
