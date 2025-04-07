using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.ToTable("service_categories");

            builder.Property(s => s.Id)
                .HasColumnName("category_id"); 

            builder.Property(s => s.Name)
                .HasColumnName("name"); 
        }
    }
}
