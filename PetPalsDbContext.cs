using Microsoft.EntityFrameworkCore;
using System.Data;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9
{
    public class PetPalsDbContext : DbContext
    {
        public PetPalsDbContext(DbContextOptions<PetPalsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<ServiceTransaction> ServiceTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new ServiceCategoryConfiguration());
            modelBuilder.Entity<Pet>()
         .Property(p => p.Price)
         .HasColumnType("DECIMAL(10,2)"); // Ensure EF Core matches DB type

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("DECIMAL(10,2)"); // Same for Service entity

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasOne(e => e.Role)
                      .WithMany()
                      .HasForeignKey(e => e.RoleId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            //modelBuilder.Entity<Species>(entity =>
            //{
            //    entity.HasKey(e => e.SpeciesId);
            //    entity.HasIndex(e => e.Name).IsUnique();
            //});

            modelBuilder.Entity<Species>()
           .ToTable("species")
           .Property(s => s.SpeciesId)
           .HasColumnName("species_id");

            //modelBuilder.Entity<Pet>(entity =>
            //{
            //    entity.HasKey(e => e.PetId);
            //    entity.Property(e => e.Price).HasPrecision(10, 2);
            //    entity.HasOne(e => e.Owner)
            //          .WithMany()
            //          .HasForeignKey(e => e.OwnerId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //    entity.HasOne(e => e.Species)
            //          .WithMany()
            //          .HasForeignKey(e => e.SpeciesId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});

            modelBuilder.Entity<Pet>()
            .ToTable("pets")
            .Property(p => p.PetId)
            .HasColumnName("pet_id");

            modelBuilder.Entity<Pet>()
                .Property(p => p.SpeciesId)
                .HasColumnName("species_id");

            modelBuilder.Entity<Pet>()
                .Property(p => p.OwnerId)
                .HasColumnName("owner_id");

            modelBuilder.Entity<Adoption>(entity =>
            {
                entity.HasKey(e => e.AdoptionId);
                entity.HasOne(e => e.Adopter)
                      .WithMany()
                      .HasForeignKey(e => e.AdopterId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Pet)
                      .WithMany()
                      .HasForeignKey(e => e.PetId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Adopter)
                .WithMany()
                .HasForeignKey(a => a.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Pet)
                .WithMany()
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adoption>()
         .Property(a => a.AdoptionDate)
         .HasColumnType("DATETIMEOFFSET");

            modelBuilder.Entity<Adoption>()
                .Property(a => a.CreatedAt)
                .HasColumnType("DATETIMEOFFSET");

            modelBuilder.Entity<Adoption>()
                .Property(a => a.UpdatedAt)
                .HasColumnType("DATETIMEOFFSET");

            modelBuilder.Entity<Adoption>()
                .Property(a => a.BookingDate)
                .HasColumnType("DATETIMEOFFSET");

            modelBuilder.Entity<ServiceTransaction>()
      .HasOne(st => st.Service)   // ✅ Reference to Service entity
      .WithMany()                 // ✅ A Service can have many transactions
      .HasForeignKey(st => st.ServiceId)  // ✅ FK is ServiceId
      .OnDelete(DeleteBehavior.Restrict); // ✅ Prevent cascade delete

            modelBuilder.Entity<ServiceTransaction>()
                .HasOne(st => st.User)  // ✅ Reference to Adopter entity
                .WithMany()                // ✅ An Adopter can have many transactions
                .HasForeignKey(st => st.AdopterId)  // ✅ FK is AdopterId
                .OnDelete(DeleteBehavior.Restrict); // ✅ Prevent cascade delete


            modelBuilder.Entity<ServiceTransaction>()
            .Property(st => st.BookingDate)
            .HasColumnType("datetimeoffset");  // ✅ Forces correct type

            modelBuilder.Entity<ServiceTransaction>()
                .Property(st => st.CreatedAt)
                .HasColumnType("datetimeoffset");

            modelBuilder.Entity<ServiceTransaction>()
                .Property(st => st.UpdatedAt)
                .HasColumnType("datetimeoffset");
            //modelBuilder.Entity<ServiceCategory>().ToTable("service_categories");
            //modelBuilder.Entity<ServiceCategory>().ToTable("service_categories");

            //modelBuilder.Entity<ServiceCategory>()
            //    .Property(p => p.CategoryId)
            //    .HasColumnName("category_id");

            //modelBuilder.Entity<ServiceCategory>().Property(p => p.Name).HasColumnName("name");

            //modelBuilder.Entity<ServiceCategory>(entity =>
            //{
            //    entity.HasKey(e => e.CategoryId);
            //    entity.HasIndex(e => e.Name).IsUnique();
            //});

            //modelBuilder.Entity<Service>().ToTable("services");
            //modelBuilder.Entity<Service>(entity =>
            //{
            //    entity.HasKey(e => e.ServiceId);
            //    entity.Property(e => e.Price).HasPrecision(10, 2);
            //    entity.HasOne(e => e.Provider)
            //          .WithMany()
            //          .HasForeignKey(e => e.ProviderId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //    entity.HasOne(e => e.Category)
            //          .WithMany()
            //          .HasForeignKey(e => e.CategoryId)
            //          .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of categories if services exist
            //});





            modelBuilder.Entity<Service>()
        .ToTable("services")  
        .Property(s => s.ServiceId)
        .HasColumnName("service_id");  

            modelBuilder.Entity<Service>()
                .Property(s => s.ProviderId)
                .HasColumnName("provider_id");

            modelBuilder.Entity<Service>()
                .Property(s => s.CategoryId)
                .HasColumnName("category_id");

            modelBuilder.Entity<Service>()
                .Property(s => s.City)
                .HasColumnName("city");

            modelBuilder.Entity<Service>()
                .Property(s => s.Address)
                .HasColumnName("address");





            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasColumnName("user_id");

         


            modelBuilder.Entity<ServiceCategory>()
       .ToTable("service_categories")  
       .Property(s => s.CategoryId)
       .HasColumnName("category_id");  

            modelBuilder.Entity<ServiceCategory>()
                .Property(s => s.Name)
                .HasColumnName("name");
            modelBuilder.Entity<ForumPost>(entity =>
            {
                entity.HasKey(e => e.ForumPostId);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ForumComment>(entity =>
            {
                entity.HasKey(e => e.ForumCommentId);
                entity.HasOne(e => e.Post)
                      .WithMany()
                      .HasForeignKey(e => e.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
