using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class AuthorWithBooks    // Listing all books for a specific Author.
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                // Listing all authors so the user can see which authors exists.
                var Authors = context.Authors.ToList();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                System.Console.WriteLine("List of Authors:");
                Console.ResetColor();
                foreach(var author in Authors)
                {
                    System.Console.WriteLine($"Author ID: {author.AuthorID, -3} - {author.FirstName} {author.LastName}");
                }

                Console.Write("\nEnter the First Name of the Author: ");
                string _input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(_input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The name cannot be empty!");
                    Console.ResetColor();
                    return;
                }

                var Books = context.Authors
                    .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                    .Where(a => a.FirstName.Contains(_input))
                    .SelectMany(a => a.BookAuthors.Select(ba => ba.Book))
                    .ToList();

                if (!Books.Any())
                {   
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"No books are matched for the author '{_input}'.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\nBooks written by '{_input}':");
                    Console.ResetColor();

                    foreach (var book in Books)
                    {
                        Console.WriteLine($"- {book.Title, -20} Genre: {book.Genre, -15} Release date: {book.ReleaseDate, -20} Publisher: {book.Publisher}");
                    }
                }
                break;
            }
        }
    }
}