using Microsoft.EntityFrameworkCore;

namespace HelloDotNetCoreWebAPI.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region DbSet
        //Đại diện cho tập thực thể về hàng hóa
        public DbSet<HangHoaEntity> HangHoas { get; set; }
        public DbSet<LoaiEntity> Loais { get; set; }
        #endregion
        public DbSet<NguoiDungEntity> NguoiDungs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HangHoaEntity>(e =>
            {
                e.ToTable("hang_hoa");
                e.HasKey(hh => hh.id);

            });
            modelBuilder.Entity<NguoiDungEntity>(entity =>
            {
                entity.HasIndex(e => e.username).IsUnique();
                entity.Property(e => e.email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.email).IsUnique();
            });
        }

    }
}
