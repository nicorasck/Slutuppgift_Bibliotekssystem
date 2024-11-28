using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Lending
    {
        public int LoanID {get; set;}           // PK => unique Key for each loan.
        public int BookID {get; set;}           // FK => references the entity of Book.
        public int BorrowerID {get; set;}       // FK => references the entity of Borrower.
        public DateTime LoanDate {get; set;}
        public DateTime ReturnDate {get; set;}
        public bool IsReturned {get; set;}      // default is False!

        public Book Book {get; set;}            // Property to the Book entity.
        public Borrower Borrower {get; set;}    // Property to the Borrower entity.
    }
}
