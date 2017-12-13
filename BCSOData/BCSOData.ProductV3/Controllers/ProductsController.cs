using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using BCSOData.ProductV3.Models;
using Microsoft.Data.OData;
using BCSOData.ProductV3.Filters;
using System.Web.Http.OData.Formatter;

namespace BCSOData.ProductV3.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using BCSOData.ProductV3.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [IdentityBasicAuthentication] // Enable authentication via an ASP.NET Identity user name and password
    [Authorize] // Require some form of authentication
    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private IList<Product> _products = new List<Product>();
        public ProductsController()
        {
            for (int i = 0; i < 1000; i++)
            {
                _products.Add(new Product()
                {
                    Id = i.ToString(),
                    Name = "Prodct" + i.ToString(),
                    Category = "Music",
                    Price = Convert.ToDecimal(1.19)
                });
            }
        }

        // GET: odata/Products
        public IHttpActionResult GetProducts(ODataQueryOptions<Product> queryOptions)
        {
            //var odataFormatters = ODataMediaTypeFormatters.Create();
            //odataFormatters = odataFormatters.Where(
            //    f => f.SupportedMediaTypes.Any(
            //        m => m.MediaType == "application/atom+xml" ||
            //            m.MediaType == "application/atomsvc+xml")).ToList();

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            //this.Request.Headers.Accept.Clear();
            //this.Request.Headers.Add("Accept", "application/atom+xml");
            IQueryable<Product> result = _products.AsQueryable();
            var keyValues = queryOptions.Request.GetQueryNameValuePairs();
            int pageSize = 10;
            int pageNumber = 0;
            if (keyValues != null)
            {
                var kvPageNumber = keyValues.FirstOrDefault(kv => kv.Key.ToLower() == "pagenumber");
                if (kvPageNumber.Equals(new KeyValuePair<string, string>()) == false)
                {
                    pageNumber = Convert.ToInt32(kvPageNumber.Value);
                }
            }
            result = result.Skip(pageSize * pageNumber).Take(pageSize);

            return Ok<IEnumerable<Product>>(result);
        }

        // GET: odata/Products(5)
        public IHttpActionResult GetProduct([FromODataUri] string key, ODataQueryOptions<Product> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            Product result = _products.FirstOrDefault(p => p.Id == key);
            return Ok<Product>(result);
        }

    }
}
