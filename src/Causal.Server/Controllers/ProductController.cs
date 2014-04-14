using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Causal.Server.ViewModels;

namespace Causal.Server.Controllers
{
    public class ProductController : ApiController
    {
        public ProductListResult Get()
        {
            return new ProductListResult
            {
                AvailableProducts = new List<string>() { "p1", "p2" }
            };
        }

        public UpdateCheckResult Get(string id)
        {
            return new UpdateCheckResult
            {
                ProductId = id,
                LatestVersion = "1.0.0.0",
                DownloadUrl = new Uri("http://example.com/file.msi")
            };
        }
    }
}