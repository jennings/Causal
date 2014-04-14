using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Causal.Server.Database;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using Owin;

namespace Causal.Server
{
    public class Startup
    {
        private static NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder appBuilder)
        {
            Log.Info("Causal Server startup");

            UpgradeDatabase();
            System.Data.Entity.Database.SetInitializer(
                new NullDatabaseInitializer<CausalContext>());

            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "SiteMap",
                routeTemplate: "",
                defaults: new { controller = "SiteMap" });

            config.Routes.MapHttpRoute(
                name: "Product",
                routeTemplate: "1/product/{id}",
                defaults: new { controller = "Product", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Ping",
                routeTemplate: "1/ping",
                defaults: new { controller = "Ping" });

            appBuilder.UseWebApi(config);
        }

        public void UpgradeDatabase()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var announcer = new TextWriterAnnouncer(s => Log.Debug(s));
            var context = new RunnerContext(announcer)
            {
                Namespace = "Causal.Server.Migrations"
            };

            var providerName = ConfigurationManager.ConnectionStrings["CausalContext"].ProviderName;
            MigrationProcessorFactory factory;
            if (providerName.Equals("Npgsql", StringComparison.InvariantCultureIgnoreCase))
            {
                factory = new FluentMigrator.Runner.Processors.Postgres.PostgresProcessorFactory();
            }
            else if (providerName.Equals("System.Data.SqlClient", StringComparison.InvariantCultureIgnoreCase))
            {
                factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
            }
            else
            {
                throw new Exception("Unknown provider for connection string CausalContext");
            }

            var options = new ProcessorOptions { PreviewOnly = false };
            var processor = factory.Create(ConfigurationManager.ConnectionStrings["CausalContext"].ConnectionString, announcer, options);
            var migrator = new MigrationRunner(assembly, context, processor);

            migrator.MigrateUp(true);
        }
    }
}