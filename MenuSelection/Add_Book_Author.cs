using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
public class AddBook    // Class for adding a Book (Create => CRUD)
{
    
    public static void Run()
    {
        // Creating a new instance of the DataBase context (Mr. Aladdin taught us that).
        using (var context = new AppDbContext())
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("\nAdd a new Book to the Library.\n");
            Console.ForegroundColor = ConsoleColor.White;

            System.Console.WriteLine("Enter a Title: ");
            var _title = Console.ReadLine();
            System.Console.WriteLine("Enter a Genre: ");
            var _genre = Console.ReadLine();
            System.Console.WriteLine("Enter a Publisher: ");
            var _publisher = Console.ReadLine();

            System.Console.WriteLine("Enter a Release Date (yyyy-MM-dd): ");
            var _releaseDate = Console.ReadLine();
            // Error handling if the user is entering in wrong format!
            if (!DateOnly.TryParse(_releaseDate, out DateOnly releaseDate))
            {
                System.Console.WriteLine("The Date format might be incorrect. Please enter: yyyy-MM-dd, thank you in advance!");
                return;
            }

            System.Console.WriteLine("This book is now available for loan!");
            bool _isAvailable = true;   // bc adding a new book to the library the book will be available fpr Loan.

            var _book = new Book    // creating a new book object
            {
                Title = _title,
                Genre = _genre,
                Publisher = _publisher,
                ReleaseDate = releaseDate,
                IsAvailable = _isAvailable
            };
            context.Books.Add(_book);   // Adding the new book into DataBase Context.
            context.SaveChanges();  // Saving the book into the Database.
            //  To let the user know that the book has been added.
            System.Console.WriteLine($"Congratulations! {_title} has been added to the library!");
        }
    }
}

public class AddAuthor  // Class for adding an Author (Create => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("\nAdd a new Author to the Library.\n");
            Console.ForegroundColor = ConsoleColor.White;

            System.Console.WriteLine("Enter a First Name: ");
            var _firstName = Console.ReadLine();
            System.Console.WriteLine("Enter a Last Name: ");
            var _lastName = Console.ReadLine();

            // Using the same structure as above in AddBook for DateOnly but this is for integers instead.
            System.Console.WriteLine("Enter a Birth Year (yyyy): ");
            var _birthYear = Console.ReadLine();
            // Error handling if the user is entering in wrong format!
            if (!int.TryParse(_birthYear, out int birthYear))
            {
                System.Console.WriteLine("The format for Birth Year might be incorrect. Please enter: yyyy, thank you in advance!");
                return;
            }

            var _author = new Author    // creating a new book object
            {
                FirstName = _firstName,
                LastName = _lastName,
                BirthYear = birthYear,
            };
            context.Authors.Add(_author);   // Adding the new book into DataBase Context.
            context.SaveChanges();  // Saving the book into the Database.
            //  To let the user know that the Author has been added.
            System.Console.WriteLine($"Congratulations! Author {_firstName} {_lastName}, born in {birthYear}, has been added to the library!");
        }
    }
}
