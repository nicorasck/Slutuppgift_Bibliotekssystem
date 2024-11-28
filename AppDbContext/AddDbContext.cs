using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;


public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Lending> Lendings { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Slutuppgift_Bibliotekssystem;Trusted_Connection=True;");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Customer>()
    //         .HasOne(c=> c.Address)
    //         .WithMany(a=> a.Customers)
    //         .HasForeignKey(c=> c.AddressId);
    // }
}