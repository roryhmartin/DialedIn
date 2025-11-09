using DialedUp.Domain.ClockEntries;
using DialedUp.Domain.Roles;
using DialedUp.Domain.UserRoles;
using DialedUp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DialedUp.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<ClockEntry> ClockEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Roles)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        
        modelBuilder.Entity<ClockEntry>()
            .HasOne(c => c.User) 
            .WithMany(u => u.ClockEntries) 
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClockEntry>()
            .HasOne(c => c.ApprovedByUser) 
            .WithMany(u => u.ApprovedClockEntries) 
            .HasForeignKey(c => c.ApprovedBy)
            .OnDelete(DeleteBehavior.SetNull);

    }
}