using GameLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameLibrary.Data;

public sealed class DefaultDbContext(Statics statics) : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(statics.GetConfiguration().GetConnectionString("Sqlite"));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Game>(eBuilder =>
		{
			eBuilder.HasKey(g => g.Id);
			eBuilder.Property(g => g.Name).HasMaxLength(GameNameMaxLength);
			eBuilder.HasMany(g => g.Genres).WithMany(g => g.Games);
		});

		modelBuilder.Entity<Genre>(eBuilder =>
		{
			eBuilder.HasKey(g => g.Id);
			eBuilder.Property(g => g.Name).HasMaxLength(GenreNameMaxLength);

			eBuilder.HasData(new()
			{
				Id = 1,
				Name = "Action"
			}, new()
			{
				Id = 2,
				Name = "Horror"
			}, new()
			{
				Id = 3,
				Name = "Open World"
			});
		});

		modelBuilder.Entity<Order>(eBuilder =>
		{
			eBuilder.HasKey(o => o.Id);
			eBuilder.HasOne(o => o.User).WithMany(u => u.Orders);
		});

		modelBuilder.Entity<OrderDetails>(eBuilder =>
		{
			eBuilder.HasKey(o => new
			{
				o.GameId,
				o.OrderId
			});
			eBuilder.HasOne(o => o.Order).WithMany(o => o.OrderDetails);
			eBuilder.HasOne(o => o.Game).WithMany(g => g.OrderDetails);
		});

		modelBuilder.Entity<User>(eBuilder =>
		{
			eBuilder.HasKey(u => u.Id);
			eBuilder.Property(u => u.Login).HasMaxLength(UserLoginMaxLength);
			eBuilder.HasIndex(u => u.Login).IsUnique();
			eBuilder.Property(u => u.Email).HasMaxLength(UserEmailMaxLength);
			eBuilder.HasIndex(u => u.Email).IsUnique();
		});
	}

	public DbSet<Game> Games { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderDetails> OrderDetails { get; set; }
	public DbSet<User> Users { get; set; }

	private const int GameNameMaxLength = 64;
	private const int GenreNameMaxLength = 32;
	private const int UserLoginMaxLength = 64;
	private const int UserEmailMaxLength = 64;
}