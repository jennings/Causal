using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace Causal.Server.Migrations
{
    [Migration(20140415220000)]
    public class _20140415_Initial : Migration
    {
        public override void Down()
        {
            IfDatabase("sqlserver")
                .Delete
                    .Table("Pings");

            IfDatabase("postgres")
                .Delete
                    .Table("pings");
        }

        public override void Up()
        {
            IfDatabase("sqlserver")
                .Create
                    .Table("Pings")
                        .InSchema("dbo")
                        .WithColumn("PingId").AsInt32().PrimaryKey().Identity()
                        .WithColumn("ComputerName").AsString(100).NotNullable()
                        .WithColumn("UpdaterId").AsGuid().NotNullable()
                        .WithColumn("PingTime").AsDateTime().NotNullable();

            IfDatabase("postgres")
                .Create
                    .Table("pings")
                        .InSchema("public")
                        .WithColumn("ping_id").AsInt32().PrimaryKey().Identity()
                        .WithColumn("computer_name").AsString(100).NotNullable()
                        .WithColumn("updater_id").AsGuid().NotNullable()
                        .WithColumn("ping_time").AsDateTime().NotNullable();
        }
    }
}