using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Borrower
    {
        public int BorrowerID {get; set;}                   // PK => unique ID for each borrower.
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}

        public ICollection<Lending> Lendings {get; set;}    // One-to-Many Property => Lendings.
    }
}
