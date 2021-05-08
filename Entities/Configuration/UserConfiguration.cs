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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
                (
                    new User
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        FirstName = "Ahmet",
                        LastName = "Yildirim",
                        Email = "ahmetyildirim@hotmail.com",
                        Password = "1234567890"
                    },
                    new User
                    {
                        Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        FirstName = "A",
                        LastName = "User",
                        Email = "user@hotmail.com",
                        Password = "1234567890"
                    }
                );
        }
    }
}
