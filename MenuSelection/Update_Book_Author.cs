using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;
using System;

public class UpdateBook // Class for updating a Book (Update => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            var Books = context.Books.ToList(); // creating a local variable to the list of books in the library.
            System.Console.WriteLine("Enter a Book ID: ");
            // Error handling if there is no Book with entered ID.
            if (!int.TryParse(Console.ReadLine(), out var bookID))
            {
                System.Console.WriteLine("The ID could not be found, please try again!");
                //  Listing all Books with ID to show the user which book can be updated.
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
        
    }
}