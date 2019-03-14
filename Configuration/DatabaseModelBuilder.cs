using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using OData.NetCore.Models;
using System;

namespace OData.NetCore.Configuration
{
    public class DatabaseModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder.EntitySet<Client>(nameof(Client))
                .EntityType
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select()
                .HasMany(c => c.Products)
                .Select()
                .Expand();

            builder.EntitySet<Product>(nameof(Product))
                .EntityType
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select();

            return builder.GetEdmModel();
        }
    }
}
