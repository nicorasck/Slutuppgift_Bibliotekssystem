using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;
using System;

public class UpdateBook // Class for updating a Book (Update => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var Books = context.Books.ToList(); // Creating a local variable to the list of Books in the library.
            System.Console.WriteLine("Enter a Book ID: ");
            // Error handling if there is no Book with entered ID.
            if (!int.TryParse(Console.ReadLine(), out var bookID))
            {
                System.Console.WriteLine("The ID could not be found, please try again!");
                //  Listing all Books with ID to show the user which Book can be updated.
                foreach (var _book in Books)
                {
                    System.Console.WriteLine($"Title: {_book.Title} - ID: {_book.BookID}");
                }
                return;
            }

            var updateBook = context.Books.Find(bookID);    // If the ID was found in the Library => new local variable is Created.
            System.Console.WriteLine($"The current Title is: {updateBook.Title}\nEnter a new Title: ");
            var _title = Console.ReadLine();
            // Error handling if the user left the field empty/null.
            if (!string.IsNullOrWhiteSpace(_title)) // this is better than IsNullOrEmpty bc the user can press 'space' and the title will be "empty".
            {
                updateBook.Title = _title; //   Updating the book title.
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
            var Authors = context.Authors.ToList(); // Creating a local variable to the list of Authors in the library.
            System.Console.WriteLine("Enter an Author ID: ");
            // Error handling if there is no Book with entered ID.
            if (!int.TryParse(Console.ReadLine(), out var authorID))
            {
                System.Console.WriteLine("The ID could not be found, please try again!");
                //  Listing all Authors with ID to show the user which Author can be updated.
                foreach (var _author in Authors)
                {
                    System.Console.WriteLine($"Author: {_author.FirstName} {_author.LastName} - ID: {_author.AuthorID}");
                }
                return;
            }

            var updateAuthor = context.Authors.Find(authorID);  // If the ID was found in the Library => new local variable is Created.
            System.Console.WriteLine($"The current Author name is: {updateAuthor.FirstName} {updateAuthor.LastName}, born in {updateAuthor.BirthYear}.");
            
            var _firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(_firstName)) 
            {
                updateAuthor.FirstName = _firstName; //   Updating the Author FirstName.
            }

            var _lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(_lastName))
            { 
                updateAuthor.LastName = _lastName; //   Updating the Author LastName.
            }

            var _birthYear = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(_birthYear))
            {
                if (int.TryParse(_birthYear, out int birthYear))
                {
                    updateAuthor.BirthYear = birthYear;
                }
                else
                {
                    System.Console.WriteLine("The format for Birth Year might be incorrect. Please enter: yyyy, thank you in advance!");
                    return;
                }

            }
        }
    }
}