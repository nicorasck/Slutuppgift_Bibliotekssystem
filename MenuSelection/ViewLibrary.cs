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
            // JOIN
            var BookAuthor = context.Books
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .ToList();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("\nLibrary - Author and their Books:\n");
            Console.ResetColor();
            foreach (var _book in BookAuthor)
            {
                //  Here is the famous JOIN-part where authors and books will be shown from the above implementation.
                var authors = string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                Console.WriteLine($"Author: {authors, -30} Book ID: {_book.BookID, -5} Title: {_book.Title, -30} Genre: {_book.Genre, -15} Publisher: {_book.Publisher}");
            }

            System.Console.WriteLine("______________________________________________________________\n");
            var loanHistory = context.Lendings
                .Include(l => l.Book)   // Entity of the Book will be included, otherwise you cannot track the history for sure.
                .ThenInclude(ba => ba.BookAuthors)  // Including BookAuthors, if there are any in the entity of Books.
                .Select(lh => new
            {
                lh.Book.Title,  // Book Title.
                lh.Borrower.BorrowerID, // Showing the ID for the borrower, in this case is not necessary to show name.
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
                    Console.WriteLine($"{"Title:", -20} {item.Title}");
                    Console.WriteLine($"{"Borrower ID:", -20} {item.BorrowerID}");
                    Console.WriteLine($"{"Loan Date:", -20} {item.LoanDate.ToShortDateString()}");
                    Console.WriteLine($"{"Return Date:", -20} {item.ReturnDate.ToShortDateString() ?? "Not Returned"}");
                    Console.WriteLine($"{"Is Returned:", -20} {(item.IsReturned ? "Yes" : "No")}");
                    System.Console.WriteLine();
                }
                System.Console.WriteLine("\n(Press any key for Menu)");
            }
            Console.ReadLine();
        }
    }
}

/*

var loanHistory = context.Lendings
    .Include(l => l.Book)   // Entity of the Book will be included, otherwise you cannot track the history for sure.
    .ThenInclude(ba => ba.BookAuthors)  // Including BookAuthors, if there are any in the entity of Books.
    .Select(lh => new
{
    lh.Book.Title,  // Book Title.
    lh.Borrower.BorrowerID, // Showing the ID for the borrower, in this case is not necessary to show name.
    lh.LoanDate,    // Date when a Book was borrowed.
    lh.ReturnDate,  // Date when a Book was returned or is going to be returned.
    lh.IsReturned   // Showing if a Book is on loan or not  => 'IsAvailable'.
}).ToList();


- - - How it will look like in SQL (when creating a new Query) - - -


SELECT
    b.Title,             -- Book Title
    l.BorrowerID,        -- Showing the ID for the borrower, in this case is not necessary to show name.
    l.LoanDate,          -- Date when a Book was borrowed.
    l.ReturnDate,        -- Date when a Book was returned or is going to be returned.
    l.IsReturned         -- Showing if a Book is on loan or not  => 'IsAvailable'
FROM
    Lendings l
JOIN
    Books AS b ON l.BookId = b.BookId   -- references 'Books'´in Lendings.
LEFT JOIN
    BookAuthors AS ba ON b.BookId = ba.BookId  -- to show any details for the book even if there are no matched Authors.

*/