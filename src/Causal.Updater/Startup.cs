using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Validation;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Owin;

namespace Causal.Updater
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "main",
                routeTemplate: "1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            appBuilder.UseWebApi(config);

            // Configure the JSON serialization settings used throughout the app.
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            JsonConvert.DefaultSettings = () => serializerSettings;
            config.Formatters.JsonFormatter.SerializerSettings = serializerSettings;

            // Remove model validation to avoid an infinte recursion when
            // using LocalTime in a request.
            // http://stackoverflow.com/a/16701145/19818
            config.Services.Clear(typeof(ModelValidatorProvider));
        }
    }
}
