using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;

public class ViewLibrary
{       
        public static void Run()
        {
            using (var context = new AppDbContext())
            {
                // JOIN
                var BookAuthor = context.Books
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                    .ToList();

                foreach (var _book in BookAuthor)
                {
                    //  Here is the famous JOIN-part where authors and books will be shown from the above implementation.
                    var authors = string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                    Console.WriteLine($"Book: {_book.Title}, Authors: {authors}");
                }
            }
        }
}