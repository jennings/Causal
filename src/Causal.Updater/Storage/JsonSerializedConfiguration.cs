using Causal.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Updater.Storage
{
    class JsonSerializedConfiguration : IConfiguration
    {
        private const string DIRECTORY = @"CausalUpdater/";
        private const string FILENAME = @"settings.json";

        private readonly string storagePath;
        private Settings settings;

        public JsonSerializedConfiguration()
        {
            var commonAppSettings = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var container = Path.Combine(commonAppSettings, DIRECTORY);
            if (!Directory.Exists(container))
                Directory.CreateDirectory(container);
            storagePath = Path.Combine(container, FILENAME);

            DiscardChanges();
        }

        public ICollection<Product> Products
        {
            get { return settings.Products; }
        }

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(storagePath, json, Encoding.UTF8);
        }

        public void DiscardChanges()
        {
            if (File.Exists(storagePath))
            {
                var json = File.ReadAllText(storagePath, Encoding.UTF8);
                settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            else
            {
                settings = new Settings();
                SaveChanges();
            }
        }

        private class Settings
        {
            public List<Product> Products { get; set; }

            public Settings()
            {
                Products = new List<Product>();
            }
        }
    }
}
