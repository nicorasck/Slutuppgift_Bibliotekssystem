
using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Book
    {
        public required int ID {get; set;}
        public required string Title {get; set;}
        public required string Genre {get; set;}
        public string ? Publisher {get; set;} // not necessary to know the Publisher
        public DateOnly ReleaseDate {get; set;}
        public bool IsAvailable = false;
    }
}
