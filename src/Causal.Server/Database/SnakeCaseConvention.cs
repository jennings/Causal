using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Humanizer;

namespace Causal.Server.Database
{
    public class SnakeCaseConvention : Convention,
        IStoreModelConvention<EntitySet>,
        IStoreModelConvention<RelationshipSet>,
        IStoreModelConvention<EdmProperty>
    {
        public void Apply(EntitySet item, DbModel model)
        {
            item.Name = item.Name.Underscore();
            item.Table = item.Table.Underscore();
        }

        public void Apply(RelationshipSet item, DbModel model)
        {
            item.Name = item.Name.Underscore();
        }

        public void Apply(EdmProperty item, DbModel model)
        {
            item.Name = item.Name.Underscore();
        }
    }
}