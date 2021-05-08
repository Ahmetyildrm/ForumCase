using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData
                (
                    new Admin
                    {
                        Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                        FirstName = "A",
                        LastName = "A",
                        Username = "Admin",
                        Email = "admin@hotmail.com",
                        Password = "adminadmin"
                    }
                );
        }
    }
}
