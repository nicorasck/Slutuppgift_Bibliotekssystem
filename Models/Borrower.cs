using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Borrower
    {
        public int BorrowerID {get; set;}                   // PK => unique ID for each borrower.
        public required string FirstName {get; set;}
        public required string LastName {get; set;}
        public required string Email {get; set;}
        public required string PhoneNumber {get; set;}

        public ICollection<Lending> Lendings {get; set;}    // One-to-Many Property => Lendings.
    }
}
