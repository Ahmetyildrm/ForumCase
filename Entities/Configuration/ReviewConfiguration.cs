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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData
                (
                    new Review
                    {
                        Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                        Content = "Deneme Content",
                        Title = "Deneme Title",
                        Star = 0,
                        Status = "Pending",
                        OparatedBy = "NoOne",
                        UserId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    }
                );
        }
    }
}
