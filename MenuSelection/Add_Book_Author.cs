using System;
using Slutuppgift_Bibliotekssystem;
public class AddBook
{
    
    public static void Run()
    {
        // Creating a new instance of the DataBase context (Mr. Aladdin taught us that).
        using (var context = new AppDbContext())
        {
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
            System.Console.WriteLine($"Congratulations! Your {_book} has been added to the library!");
        }
    }
}

public class AddAuthor
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {

        }

    }

}
