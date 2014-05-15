using Causal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Causal.Updater.Storage
{
    interface IConfiguration
    {
        ICollection<Product> Products { get; }
        void DiscardChanges();
        void SaveChanges();
    }
}
