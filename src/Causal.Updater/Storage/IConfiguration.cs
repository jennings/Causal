using Causal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Causal.Updater.Storage
{
    interface IConfiguration
    {
        List<Product> Products { get; }
        void SaveProduct(Product product);
        void DeleteProduct(Product product);
    }
}
