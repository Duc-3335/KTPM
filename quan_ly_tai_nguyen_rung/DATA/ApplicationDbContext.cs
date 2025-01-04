using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA.@enum;
using quan_ly_tai_nguyen_rung.Models.section1;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Models.section3;
using quan_ly_tai_nguyen_rung.Models.section4;


namespace quan_ly_tai_nguyen_rung.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<AccessHistory> AccessHistories { get; set; }
        public DbSet<ImpactHistory> ImpactHistoris { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<RolesGroup> RolesGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<PlantFacility> PlantFacilities { get; set; }
        public DbSet<PlantType> PlantTypes { get; set; }
        public DbSet<PlantTypePlantFacility> plantPlantFacilities { get; set; }
        public DbSet<WoodProcessingFacility> WoodProcessingFacilities { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalFacility> AnimalFacilities { get; set; }
        public DbSet<AnimalAnimalFacility> animalAnimalFacilities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalAnimalFacility>()
                .HasOne(aaf => aaf.AnimalFacility)
                .WithMany(af => af.animalAnimalFacilities)
                .HasForeignKey(aaf => aaf.AnimalFacilityId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa bản ghi liên quan tự động

            modelBuilder.Entity<AnimalAnimalFacility>()
                .HasOne(aaf => aaf.Animal)
                .WithMany(a => a.animalAnimalFacilities)
                .HasForeignKey(aaf => aaf.AnimalId)
                .OnDelete(DeleteBehavior.Restrict); // Hạn chế xóa Animal nếu còn liên kết
        }

    }
}
