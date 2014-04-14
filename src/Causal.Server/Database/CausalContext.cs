using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Causal.Model;

namespace Causal.Server.Database
{
    public class CausalContext : DbContext
    {
        static NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public CausalContext()
        {
            Database.Log = sql => Log.Debug(sql);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (Database.Connection.GetType() == typeof(Npgsql.NpgsqlConnection))
            {
                modelBuilder.HasDefaultSchema("public");
                modelBuilder.Conventions.Add<SnakeCaseConvention>();
            }
        }

        public IDbSet<Ping> Pings { get; set; }
    }
}