using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;
using System;
using System.Linq;

namespace MenuSelection;
public class UpdateBook // Class for updating a Book (Update => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                
                var Books = context.Books.ToList(); // Creating a local variable to the list of Books in the library.
                System.Console.WriteLine("\nEnter a Book ID (type 'LIST' to view all books or 'Q' to quit):");
                var _input = Console.ReadLine()?.Trim();

                // If the user would like to see the books before updating.
                if (_input?.ToUpper() == "LIST")
                {
                    //  Listing all Books with ID to show the user which Book can be updated.
                    foreach (var _book in Books)
                    {
                        System.Console.WriteLine($"Book ID: {_book.BookID, -3} -Title: {_book.Title}");
                    }
                    continue;
                }

                // If the user would like to exit.
                if (_input?.ToUpper() == "Q")
                {
                    System.Console.WriteLine("Redirecting to Menu for Maintenance.\n");
                    break;
                }

                // Error handling if there is no Book with entered ID.
                if (!int.TryParse(_input, out var bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                var updateBook = context.Books.Find(bookID);    // If the ID was found in the Library => new local variable is Created.
                // Error handling.
                if(updateBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                System.Console.WriteLine($"\nThe current Title is: {updateBook.Title}\nEnter a new Title: ");
                var _title = Console.ReadLine();
                // Error handling if the user left the field empty/null.
                if (!string.IsNullOrWhiteSpace(_title)) // this is better than IsNullOrEmpty bc the user can press 'space' and the title will be "empty".
                {
                    updateBook.Title = _title; //   Updating the book title.
                    context.SaveChanges();  // Saving the new Title for the Book.
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    System.Console.WriteLine($"\nYou've now renamed the book to {_title}.\n");
                    Console.ResetColor();
                }
                return;
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
                System.Console.WriteLine("\nEnter a Book ID (type 'LIST' to view all books or 'Q' to quit):");
                var _input = Console.ReadLine()?.Trim();

                // If the user would like to see the books before updating.
                if (_input?.ToUpper() == "LIST")
                {
                    //  Listing all Books with ID to show the user which Book can be updated.
                    foreach (var _author in Authors)
                    {
                        System.Console.WriteLine($"Author ID: {_author.AuthorID,3} - Title: {_author.FirstName} {_author.LastName}");
                    }
                    continue;
                }

                // If the user would like to exit.
                if (_input?.ToUpper() == "Q")
                {
                    System.Console.WriteLine("Redirecting to Menu for Maintenance.\n");
                    break;
                }
   
                // Error handling if there is no Book with entered ID.
                if (!int.TryParse(_input, out var authorID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                var updateAuthor = context.Authors.Find(authorID);  // If the ID was found in the Library => new local variable is Created.
                // Error handling.
                if (updateAuthor == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                System.Console.WriteLine($"\nThe current Author name is: {updateAuthor.FirstName} {updateAuthor.LastName}, born in {updateAuthor.BirthYear}.");
                System.Console.WriteLine("Enter a new First Name:");
                var _firstName = Console.ReadLine().Trim();
                // Error handling if the user left the field empty/null.
                if (!string.IsNullOrWhiteSpace(_firstName)) 
                {
                    updateAuthor.FirstName = _firstName; //   Updating the Author FirstName.
                }
                System.Console.WriteLine("Enter a new Last Name:");
                // Error handling if the user left the field empty/null.
                var _lastName = Console.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(_lastName))
                { 
                    updateAuthor.LastName = _lastName; //   Updating the Author LastName.
                }
                System.Console.WriteLine("Enter a new Birth Year (yyyy)");
                // Error handling if the user left the field empty/null.
                var _birthYear = Console.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(_birthYear))
                {
                    if (int.TryParse(_birthYear, out int birthYear))
                    {
                        updateAuthor.BirthYear = birthYear;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\nThe format for Birth Year might be incorrect. Please enter: yyyy, thank you in advance!");
                        Console.ResetColor();
                        continue;
                    }
                }
                context.SaveChanges();  // Saving the new bio for the Author.
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                System.Console.WriteLine($"\nYou've now changed the name to {_firstName} {_lastName}.\n");
                Console.ResetColor();
                return;
            }
        }
    }
}