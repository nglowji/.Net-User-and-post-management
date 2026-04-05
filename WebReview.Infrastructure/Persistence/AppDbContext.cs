using Microsoft.EntityFrameworkCore;
using WebReview.Domain.Constants;
using WebReview.Domain.Entities;

namespace WebReview.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Username).IsRequired().HasMaxLength(100);
            entity.Property(x => x.PasswordHash).IsRequired();
            entity.Property(x => x.Role).IsRequired().HasMaxLength(20).HasDefaultValue(UserRoles.User);
            entity.HasIndex(x => x.Username).IsUnique();
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Content).IsRequired();

            entity.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}