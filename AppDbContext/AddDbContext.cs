using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;


public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }          // A list to store Authors.
    public DbSet<Book> Books { get; set; }              // A list to store Books.
    public DbSet<BookAuthor> BookAuthors { get; set; }  // Bridge Table => Many-to-Many (Books <-> Author).
    public DbSet<Borrower> Borrowers { get; set; }      // A list to store Borrowers.
    public DbSet<Lending> Lendings { get; set; }        // A list to store Loans.
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Slutuppgift_Bibliotekssystem;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
        .HasKey(baid => baid.BookAuthorID);      // this is a composite Key for the bridge table.
        
        modelBuilder.Entity<BookAuthor>()
        .HasOne(b => b.Book)                    // BookAuthor is related to one Book.
        .WithMany(ba => ba.BookAuthors)         // A book can have several of BookAuthors.
        .HasForeignKey(bid => bid.BookID);      // FK => BookID.

        modelBuilder.Entity<BookAuthor>()
        .HasOne(a => a.Author)                  // BookAuthor is related to one Author.
        .WithMany(ba => ba.BookAuthors)         // Aa Author can have several of BookAuthors.
        .HasForeignKey(aid => aid.AuthorID);    // FK => AuthorID.
    }
}