using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.ToTable("service_categories"); // Match DB table name

            builder.Property(s => s.CategoryId)
                .HasColumnName("category_id"); // Match DB column name

            builder.Property(s => s.Name)
                .HasColumnName("name"); // Match DB column name
        }
    }
}
