using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class LoanHistory    // A method for listing LoanHistory => called in the classes for ViewLibrary.
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                var _loanHistory = context.Lendings
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

                if (!_loanHistory.Any())
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

                    foreach (var item in _loanHistory)
                    {
                        // Adjust the width for each column as necessary
                        Console.WriteLine($"{"Title:",-20} {item.Title} (ID: {item.BookID})");
                        Console.WriteLine($"{"Borrower:",-20} {item.FirstName} {item.LastName} (ID: {item.BorrowerID})");
                        Console.WriteLine($"{"Loan Date:",-20} {item.LoanDate.ToShortDateString()}");
                        Console.WriteLine($"{"Return Date:",-20} {item.ReturnDate.ToShortDateString() ?? "Not Returned"}");
                        Console.WriteLine($"{"Is Returned:",-20} {(item.IsReturned ? "Yes" : "No")}");
                        System.Console.WriteLine();
                    }
                    System.Console.WriteLine("(Press Enter for Menu)");
                }
                Console.ReadLine();
                break;
            }
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