using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.Models.Security;
using ProjectsMeasurements.Models.BasicData;
using ProjectsMeasurements.Models.Operations;
using Microsoft.Extensions.Configuration;

namespace ProjectsMeasurements.DBContext
{
    public class ProjectsMeasurementsDBContext : DbContext
    {

        #region Members

        private readonly IConfiguration _config;

        #endregion

        #region Constructor

        public ProjectsMeasurementsDBContext(DbContextOptions<ProjectsMeasurementsDBContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        #endregion

        #region Models

        #region Security

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupsPermission> GroupsPermissions { get; set; }
        public DbSet<UsersGroup> UsersGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionsType> PermissionsTypes { get; set; }

        #endregion

        #region Basic Data

        public DbSet<PlantsCategory> PlantsCategories { get; set; }
        public DbSet<Unit> Units { get; set; }

        #endregion

        #region Operations

        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantsDetail> PlantsDetails { get; set; }
        public DbSet<MeasurementsHeader> MeasurementsHeaders { get; set; }
        public DbSet<MeasurementsDetail> MeasurementsDetails { get; set; }
        public DbSet<MeasurementsType> MeasurementsTypes { get; set; }

        #endregion

        #endregion

        #region Overrided Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DBConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("Arabic_CI_AS");
            modelBuilder.Entity<User>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<MeasurementsType>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Group>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<UsersGroup>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Permission>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<PermissionsType>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<GroupsPermission>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Project>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Owner>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Contractor>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Unit>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<PlantsCategory>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Plant>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<PlantsDetail>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<MeasurementsHeader>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<MeasurementsDetail>().Property(prp => prp.LastUpdateDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<MeasurementsHeader>().Property(prp => prp.MeasurementDate).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<User>().HasIndex(x => x.UserName);
            modelBuilder.Entity<User>().HasIndex(x => x.UserEmail);
            modelBuilder.Entity<User>().HasOne(x=>x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Group>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UsersGroup>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Permission>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PermissionsType>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PlantsCategory>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Unit>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Contractor>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Project>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Owner>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Plant>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PlantsDetail>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MeasurementsHeader>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MeasurementsDetail>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MeasurementsType>().HasOne(x => x.LastUpdateUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Project>().HasOne(x => x.ParentProject).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PlantsCategory>().HasOne(x => x.ParentCategory).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Permission>().HasOne(x => x.ParentPermission).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Permission>().HasOne(x => x.TypeOfPermission).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Plant>().HasOne(x => x.CategoryOfPlant).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PlantsDetail>().HasOne(x=>x.PlantHeightUnit).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MeasurementsHeader>().HasOne(x => x.MeasurementType).WithMany().OnDelete(DeleteBehavior.Restrict);
        }

        #endregion

    }
}