using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Causal.Model.Updater.v1;
using Causal.Updater.Updates;
using Causal.Updater.Storage;
using Causal.Model;
using System.Net;

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
            var product =
                this.configuration.Products
                .FirstOrDefault(
                    p => p.ProductId.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            if (product == null)
            {
                product = new Product();
                this.configuration.Products.Add(product);
            }

            if (id.Equals(request.ProductId, StringComparison.InvariantCultureIgnoreCase))
                product.ProductId = request.ProductId;
            this.configuration.SaveChanges();
            return product;
        }

        public void Delete(string id)
        {
            var product =
                this.configuration.Products
                .SingleOrDefault(p => p.ProductId.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this.configuration.Products.Remove(product);
            this.configuration.SaveChanges();
        }
    }
}
