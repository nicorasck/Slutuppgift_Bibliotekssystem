using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Author
    {
        public int AuthorID {get; set;}     // PK => unique ID for each Author.
        public required string FirstName {get; set;}
        public required string LastName {get; set;}
        public int ? BirthYear {get; set;}   // can be null => '?'

        public ICollection<BookAuthor> BookAuthors {get; set;}  // Many-to-Many Property => Books.
    }
}
