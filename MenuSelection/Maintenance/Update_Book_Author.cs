using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;
using System;
using System.Linq;

public class UpdateBook // Class for updating a Book (Update => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                var Books = context.Books.ToList(); // Creating a local variable to the list of Books in the library.
                System.Console.Write("\nEnter a Book ID: ");
                // Error handling if there is no Book with entered ID.
                if (!int.TryParse(Console.ReadLine(), out var bookID))
                {
                    System.Console.WriteLine("\nThe ID could not be found, please try again!");
                    //  Listing all Books with ID to show the user which Book can be updated.
                    foreach (var _book in Books)
                    {
                        System.Console.WriteLine($"Title: {_book.Title} - ID: {_book.BookID}");
                    }
                    continue;
                }

                var updateBook = context.Books.Find(bookID);    // If the ID was found in the Library => new local variable is Created.
                // Error handling.
                if(updateBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                System.Console.WriteLine($"\nThe current Title is: {updateBook.Title}\nEnter a new Title: ");
                var _title = Console.ReadLine();
                // Error handling if the user left the field empty/null.
                if (!string.IsNullOrWhiteSpace(_title)) // this is better than IsNullOrEmpty bc the user can press 'space' and the title will be "empty".
                {
                    updateBook.Title = _title; //   Updating the book title.
                    context.SaveChanges();  // Saving the new Title for the Book.
                    System.Console.WriteLine($"\nYou've now renamed the book to {_title}.");
                }
                Console.ReadLine();
                break;
            }
        }
    }
}

public class UpdateAuthor   // Class for updating an Author (Update => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                var Authors = context.Authors.ToList(); // Creating a local variable to the list of Authors in the library.
                System.Console.Write("\nEnter an Author ID: ");
                // Error handling if there is no Book with entered ID.
                if (!int.TryParse(Console.ReadLine(), out var authorID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!");
                    Console.ResetColor();
                    //  Listing all Authors with ID to show the user which Author can be updated.
                    foreach (var _author in Authors)
                    {
                        System.Console.WriteLine($"Author: {_author.FirstName} {_author.LastName} - ID: {_author.AuthorID}");
                    }
                    continue;
                }

                var updateAuthor = context.Authors.Find(authorID);  // If the ID was found in the Library => new local variable is Created.
                // Error handling.
                if (updateAuthor == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!");
                    Console.ResetColor();
                    continue;
                }

                System.Console.WriteLine($"\nThe current Author name is: {updateAuthor.FirstName} {updateAuthor.LastName}, born in {updateAuthor.BirthYear}.");
                // cw => ask
                var _firstName = Console.ReadLine();
                // Error handling if the user left the field empty/null.
                if (!string.IsNullOrWhiteSpace(_firstName)) 
                {
                    updateAuthor.FirstName = _firstName; //   Updating the Author FirstName.
                }
                // Error handling if the user left the field empty/null.
                var _lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(_lastName))
                { 
                    updateAuthor.LastName = _lastName; //   Updating the Author LastName.
                }
                // Error handling if the user left the field empty/null.
                var _birthYear = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(_birthYear))
                {
                    if (int.TryParse(_birthYear, out int birthYear))
                    {
                        updateAuthor.BirthYear = birthYear;
                    }
                    else
                    {
                        System.Console.WriteLine("\nThe format for Birth Year might be incorrect. Please enter: yyyy, thank you in advance!");
                        continue;
                    }
                }
                context.SaveChanges();  // Saving the new bio for the Author.
                System.Console.WriteLine($"\nYou've now changed the name to {_firstName} {_lastName}.");
                Console.ReadLine();
                return;
            }
        }
    }
}