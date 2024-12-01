
using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Book
    {
        public int BookID {get; set;}               // PK => unique ID for each book.
        public required string Title {get; set;}
        public required string Genre {get; set;}
        public string Publisher {get; set;}         // not necessary to know the Publisher
        public DateOnly? ReleaseDate {get; set;}    // DateTime can be null => '?'
        public bool IsAvailable {get; set;}         // default is False!

        public ICollection<BookAuthor> BookAuthors {get; set;}  // Many-to-Many property => Authors.
        public ICollection<Lending> Lendings {get; set;}        // One-to-Many Property => Lendings.
    }
}
