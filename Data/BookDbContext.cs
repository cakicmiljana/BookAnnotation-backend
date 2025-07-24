namespace backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

public class BookDbContext : DbContext {

    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Version> Versions { get; set; }
    
    public DbSet<Book> Books { get; set; }

    public DbSet<Annotation> Annotations { get; set; }

    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
