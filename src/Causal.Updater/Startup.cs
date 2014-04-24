﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
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
                routeTemplate: "1/{controller}/{productId}",
                defaults: new { productId = RouteParameter.Optional }
                );

            appBuilder.UseWebApi(config);
        }
    }
}
