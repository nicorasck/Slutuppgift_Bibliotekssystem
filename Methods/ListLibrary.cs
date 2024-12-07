using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ListLibrary    // A method for listing Library => called in the classes for ViewLibrary and SetRelationships.
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
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
                        Console.WriteLine($"ID: {_author.AuthorID,-3} - Author: {authors,-30} Book ID: {_book.BookID,-3} - Title: {_book.Title,-30} Genre: {_book.Genre,-15} Publisher: {_book.Publisher}");
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
                System.Console.WriteLine($"\nTotal Number of Books: {_NoOfBooks}");
                Console.ResetColor();
                break;
            }
        }
    }
}