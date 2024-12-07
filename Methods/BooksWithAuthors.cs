using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class BooksWithAuthors
{
    public static void Run()
    {

        using (var context = new AppDbContext())
        {
            while (true)
            {
                // Listing all books!
                var Books = context.Books.ToList();
                foreach(var book in Books)
                {
                    System.Console.WriteLine($"Book ID: {book.BookID, -3} - Title: {book.Title}");
                }

                Console.Write("\nEnter the book title: ");
                string _input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(_input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The book name cannot be empty!");
                    Console.ResetColor();
                    return;
                }
                var Authors = context.Books
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                    .Where(b => b.Title.Contains(_input))
                    .SelectMany(b => b.BookAuthors.Select(ba => ba.Author))
                    .ToList();

                if (!Authors.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"No Authors are matched with any books '{_input}'.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\nAuthors of the book '{_input}':");
                    Console.ResetColor();

                    foreach (var author in Authors)
                    {
                        Console.WriteLine($"Author Name: {author.FirstName} {author.LastName} (Birth year: {author.BirthYear})");
                    }
                }
                break;
            }
        }
    }
}