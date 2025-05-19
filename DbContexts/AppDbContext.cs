using Microsoft.EntityFrameworkCore;
using dovandung0300467.Entities;

namespace dovandung0300467.DbContexts;

public class AppDbContext : DbContext
{
	public DbSet<Store> Stores { get; set; }
	public DbSet<Supplier> Suppliers { get; set; }
	public DbSet<StoreSupplier> StoreSuppliers { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Định nghĩa khóa chính composite cho StoreSupplier
		modelBuilder.Entity<StoreSupplier>()
			.HasKey(ss => new { ss.StoreId, ss.SupplierId });

		// Định nghĩa quan hệ khóa ngoại
		modelBuilder.Entity<StoreSupplier>()
			.HasOne(ss => ss.Store)
			.WithMany(s => s.StoreSuppliers)
			.HasForeignKey(ss => ss.StoreId);

		modelBuilder.Entity<StoreSupplier>()
			.HasOne(ss => ss.Supplier)
			.WithMany(s => s.StoreSuppliers)
			.HasForeignKey(ss => ss.SupplierId);

		// Đảm bảo tên cửa hàng và nhà cung cấp là duy nhất
		modelBuilder.Entity<Store>()
			.HasIndex(s => s.Name)
			.IsUnique();

		modelBuilder.Entity<Supplier>()
			.HasIndex(s => s.Name)
			.IsUnique();
	}
}