using Akavache;
using Akavache.Sqlite3;
using Causal.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Updater.Storage
{
    class AkavacheConfiguration : IConfiguration
    {
        private const string DIRECTORY = @"CausalUpdater/";
        private const string FILENAME = @"settings.db";

        private const string PRODUCTS_KEY = "Products";

        private readonly SQLitePersistentBlobCache cache;

        public AkavacheConfiguration()
        {
            var commonAppSettings = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var container = Path.Combine(commonAppSettings, DIRECTORY);
            if (!Directory.Exists(container))
                Directory.CreateDirectory(container);
            var storagePath = Path.Combine(container, FILENAME);
            this.cache = new Akavache.Sqlite3.SQLitePersistentBlobCache(storagePath);
        }

        public List<Product> Products
        {
            get
            {
                return this.cache.GetOrCreateObject(PRODUCTS_KEY, () => new List<Product>()).First();
            }
        }

        public void SaveProduct(Product newProduct)
        {
            var products = Products;
            products.RemoveAll(p => p.ProductId.Equals(newProduct.ProductId, StringComparison.InvariantCultureIgnoreCase));
            products.Add(newProduct);
            this.cache.InsertObject(PRODUCTS_KEY, products);
        }

        public void DeleteProduct(Product newProduct)
        {
            var products = Products;
            products.RemoveAll(p => p.ProductId.Equals(newProduct.ProductId, StringComparison.InvariantCultureIgnoreCase));
            this.cache.InsertObject(PRODUCTS_KEY, products);
        }
    }
}
