using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ViewLibrary    // Class to read all data in the Library (Read  => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
#region Library            
            // Variable - Books and Authors which are set in a relationship.
            var BookAuthor = context.Books
                .Include(ba => ba.BookAuthors)  // From this one you get for an example the Author ID.
                .ThenInclude(a => a.Author)
                .ToList();
            
            // Variable - Authors without any connected Books.
            var AuthorsWithoutBooks = context.Authors
                .Where(a => !a.BookAuthors.Any())
                .ToList();
            // Variable - Books without any connected Authors.
            var BooksWithoutAuthors = context.Books
                .Where(b => !b.BookAuthors.Any())
                .ToList();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("\nLibrary - Author and their Books:\n");
            Console.ResetColor();
            foreach (var _book in BookAuthor)
            {
                foreach (var _author in _book.BookAuthors)
                {
                    //  Here is the famous JOIN-part where authors and books will be shown from the above implementation.
                    var authors = string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                    Console.WriteLine($"ID: {_author.AuthorID, -3} - Author: {authors,-30} Book ID: {_book.BookID,-3} - Title: {_book.Title,-30} Genre: {_book.Genre,-15} Publisher: {_book.Publisher}");
                }
            }

            // Listing the Authors without Books.
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("\nLibrary - Authors Without Books:\n");
            Console.ResetColor();
            foreach (var author in AuthorsWithoutBooks)
            {
                Console.WriteLine($"ID: {author.AuthorID,-3} - Author: {author.FirstName} {author.LastName}");
            }

            // Listing the Books without Authors.
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("\nLibrary - Books Without Authors:\n");
            Console.ResetColor();
            foreach (var book in BooksWithoutAuthors)
            {
                Console.WriteLine($"ID: {book.BookID,-3} - Title: {book.Title,-30} Genre: {book.Genre,-15} Publisher: {book.Publisher}");
            }

            // Listing the total number of books in the Library.
            var _NoOfBooks = BookAuthor.Count;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.WriteLine($"\nTotal Number of Books: {_NoOfBooks}\n");
            Console.ResetColor();
#endregion

#region LoanHistory

            var loanHistory = context.Lendings
                .Include(l => l.Book)   // Entity of the Book will be included, otherwise you cannot track the history for sure.
                .ThenInclude(ba => ba.BookAuthors)  // Including BookAuthors, if there are any in the entity of Books.
                .Select(lh => new
                {
                    lh.Book.Title,  // Book Title.
                    lh.BookID,  // Book ID
                    lh.Borrower.BorrowerID, // Showing the ID for the borrower, in this case is not necessary to show name.
                    lh.Borrower.FirstName, // Showing the borrower first name.
                    lh.Borrower.LastName, // Showing the borrower last name.
                    lh.LoanDate,    // Date when a Book was borrowed.
                    lh.ReturnDate,  // Date when a Book was returned or is going to be returned.
                    lh.IsReturned   // Showing if a Book is on loan or not  => 'IsAvailable'.
                }).ToList();

            if (!loanHistory.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No history match found for Loan.");
                Console.ResetColor();
                System.Console.WriteLine("(Press any key for Menu)\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\nLoan History.\n");
                Console.ResetColor();

                foreach (var item in loanHistory)
                {
                    // Adjust the width for each column as necessary
                    Console.WriteLine($"{"Title:",-20} {item.Title} (ID: {item.BookID})");
                    Console.WriteLine($"{"Borrower:",-20} {item.FirstName} {item.LastName} (ID: {item.BorrowerID})");
                    Console.WriteLine($"{"Loan Date:",-20} {item.LoanDate.ToShortDateString()}");
                    Console.WriteLine($"{"Return Date:",-20} {item.ReturnDate.ToShortDateString() ?? "Not Returned"}");
                    Console.WriteLine($"{"Is Returned:",-20} {(item.IsReturned ? "Yes" : "No")}");
                    System.Console.WriteLine();
                }
                System.Console.WriteLine("\n(Press any key for Menu)");
            }
            Console.ReadLine();
#endregion
        }
    }
}

#region SQL_Notes
/*

var loanHistory = context.Lendings
    .Include(l => l.Book)   // Entity of the Book will be included, otherwise you cannot track the history for sure.
    .ThenInclude(ba => ba.BookAuthors)  // Including BookAuthors, if there are any in the entity of Books.
    .Select(lh => new
{
    lh.Book.Title,  // Book Title.
    lh.BookID,  // Book ID
    lh.Borrower.BorrowerID, // Showing the ID for the borrower, in this case is not necessary to show name.
    lh.Borrower.FirstName,  // Showing the borrower first name.
    lh.Borrower.LastName,   // Showing the borrower last name.
    lh.LoanDate,    // Date when a Book was borrowed.
    lh.ReturnDate,  // Date when a Book was returned or is going to be returned.
    lh.IsReturned   // Showing if a Book is on loan or not  => 'IsAvailable'.
}).ToList();


- - - How it will look like in SQL (when creating a new Query) - - -


SELECT
    b.Title,                -- Book Title
    b.BookID,               -- Book ID
    bo.BorrowerID,          -- Showing the ID for the borrower, in this case is not necessary to show name.
    bo.BorrowerFirstName,   -- Showing the borrower first name.
    bo.BorrowerLastName,    -- Showing the borrower last name.
    l.LoanDate,             -- Date when a Book was borrowed.
    l.ReturnDate,           -- Date when a Book was returned or is going to be returned.
    l.IsReturned            -- Showing if a Book is on loan or not  => 'IsAvailable'
FROM
    Lendings l
INNER JOIN
    Books b ON l.BookId = b.BookID              -- Showing Lendings table with Books table.
LEFT JOIN
    BookAuthors ba ON b.BookID = ba.BookID      -- Showing Books table with BookAuthors table.
LEFT JOIN
    Authors a ON ba.AuthorID = a.AuthorID       -- Showing BookAuthors table with Authors table.
INNER JOIN
    Borrowers bo ON l.BorrowerID = bo.BorrowerID -- Showing Lendings table with Borrowers table

*/
#endregion