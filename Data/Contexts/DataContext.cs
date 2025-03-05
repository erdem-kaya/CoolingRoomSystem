using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<BookingsEntity> Bookings { get; set; }
    public DbSet<CoolingRoomEntity> CoolingRooms { get; set; }
    public DbSet<CoolingRoomPriceControlEntity> CoolingRoomPriceControl { get; set; }
    public DbSet<CoolingRoomStatusEntity> CoolingRoomStatus { get; set; }
    public DbSet<CustomersEntity> Customers { get; set; }
    public DbSet<PaymentStatusEntity> PaymentStatus { get; set; }
    public DbSet<PaymentsEntity> Payments { get; set; }
    public DbSet<UsersEntity> Users { get; set; }
    public DbSet<RoleEntity> Role { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ Bookings - Payments: One-to-One ilişki
        modelBuilder.Entity<BookingsEntity>()
            .HasOne(b => b.Payment)
            .WithOne()
            .HasForeignKey<BookingsEntity>(b => b.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Bookings - Customers
        modelBuilder.Entity<BookingsEntity>()
            .HasOne(b => b.Customer)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // ✅ Bookings - CoolingRooms
        modelBuilder.Entity<BookingsEntity>()
            .HasOne(b => b.CoolingRoom)
            .WithMany()
            .HasForeignKey(b => b.CoolingRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        // ✅ Bookings - Users
        modelBuilder.Entity<BookingsEntity>()
            .HasOne(b => b.BookedByUser)
            .WithMany()
            .HasForeignKey(b => b.BookedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ✅ CoolingRooms - CoolingRoomStatus
        modelBuilder.Entity<CoolingRoomStatusEntity>()
            .HasMany(s => s.CoolingRooms)
            .WithOne(c => c.RoomStatus)
            .HasForeignKey(c => c.RoomStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ CoolingRooms - CoolingRoomPriceControl
        modelBuilder.Entity<CoolingRoomEntity>()
            .HasOne(c => c.RoomPrice)
            .WithMany()
            .HasForeignKey(c => c.RoomPriceId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Payments - PaymentStatus
        modelBuilder.Entity<PaymentsEntity>()
            .HasOne(p => p.PaymentStatus)
            .WithMany()
            .HasForeignKey(p => p.PaymentStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // ✅ Payments - Customers
        modelBuilder.Entity<CustomersEntity>()
            .HasMany(c => c.Payments)
            .WithOne(p => p.Customers)
            .HasForeignKey(p => p.CustomerPaymentId)
            .OnDelete(DeleteBehavior.Cascade);

        // ✅ Users - Role
        modelBuilder.Entity<UsersEntity>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
