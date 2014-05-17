using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Causal.Model;
using Causal.Model.Updater.v1;
using Causal.Updater.Storage;
using Causal.Updater.Updates;

namespace Causal.Updater.v1
{
    public class ProductController : ApiController
    {
        private readonly IConfiguration configuration;

        public ProductController()
        {
            this.configuration = new JsonSerializedConfiguration();
        }

        public IEnumerable<Product> Get()
        {
            return this.configuration.Products;
        }

        public Product Get(string id)
        {
            return this.configuration.Products.SingleOrDefault(p => p.ProductId.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }

        public Product Post(string id, [FromBody]Product request)
        {
            if (String.IsNullOrWhiteSpace(id))
                throw HttpResponseFactory.BadRequest("Product ID must be provided.");
            if (request == null)
                throw HttpResponseFactory.BadRequest("Product must be included in the body.");

            var product =
                this.configuration.Products
                .FirstOrDefault(
                    p => p.ProductId.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            if (product == null)
            {
                product = new Product();
                product.ProductId = id;
                this.configuration.Products.Add(product);
            }

            // Update the casing of the ProductId if it was sent
            if (id.Equals(request.ProductId, StringComparison.InvariantCultureIgnoreCase))
                product.ProductId = request.ProductId;

            product.Schedule = request.Schedule;
            if (product.Schedule == null)
                product.Schedule = new Schedule();

            this.configuration.SaveChanges();
            return product;
        }

        public void Delete(string id)
        {
            var product =
                this.configuration.Products
                .SingleOrDefault(p => p.ProductId.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            if (product == null)
                throw HttpResponseFactory.NotFound();

            this.configuration.Products.Remove(product);
            this.configuration.SaveChanges();
        }
    }
}
