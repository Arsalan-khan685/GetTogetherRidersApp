using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Get_Together_Riders.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
     //   public virtual DbSet<ClsUser>? Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //    }
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        //    modelBuilder.Entity<ClsUser>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.ToTable("tbl_user");

        //        entity.Property(e => e.UserID)
        //            .ValueGeneratedOnAdd()
        //            .HasColumnName("UserID");

        //        entity.Property(e => e.UserName).HasMaxLength(50);

        //        entity.Property(e => e.Password).HasMaxLength(25);

        //        entity.Property(e => e.LoginUser_ID).HasMaxLength(50);

        //        entity.Property(e => e.Bio).HasMaxLength(25);

        //        entity.Property(e => e.Email).HasMaxLength(10);

        //        entity.Property(e => e.PhoneNo).HasMaxLength(25);
        //        entity.Property(e => e.BikeModel).HasMaxLength(25);
        //        entity.Property(e => e.Location).HasMaxLength(25);
        //        entity.Property(e => e.EmergencyContactPerson).HasMaxLength(25);
        //        entity.Property(e => e.EmergencyContactNumber).HasMaxLength(25);
        //        entity.Property(e => e.RiderNo).HasMaxLength(25);
        //        entity.Property(e => e.IsActive).HasMaxLength(25);
        //    });
        //    OnModelCreatingPartial(modelBuilder);

        //}
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}