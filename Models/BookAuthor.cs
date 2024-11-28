using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class BookAuthor // bridge table
    {
        public int BookAuthorID {get; set;}   // PK => unique ID.
        public int BookID {get; set;}   // FK => references the entity of Book.
        public int AuthorID {get; set;} // FK => references the entity of Author.

        public Book Book {get; set;}    // Property to the Book entity.
        public Borrower Borrower {get; set;}    // Property to the Borrower entity.
    }
}
