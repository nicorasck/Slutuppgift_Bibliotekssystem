using System;
using Slutuppgift_Bibliotekssystem;

public class SetRelationship
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while(true)
            {
                //  Instructions for the user.
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine("\nTo establish a relationship between a Book and an Author you need to enter the specific ID's for each one.");
                Console.ResetColor();
                ListLibrary.Run();  // bringing the Library to show the user which ID each books and Authors have.
                System.Console.WriteLine();

                System.Console.WriteLine("1. Enter a Book ID: ");
                //  Error handling
                if (!int.TryParse(Console.ReadLine(), out int bookID))
                {
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    continue;
                }

                System.Console.WriteLine("2. Enter an Author ID: ");
                //  Error handling
                if (!int.TryParse(Console.ReadLine(), out int authorID))
                {
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    continue;
                }

                //  creating a new instance for the new relationship between Book and Author.
                var bookAuthor = new BookAuthor {BookID = bookID, AuthorID = authorID};

                context.BookAuthors.Add(bookAuthor);    // Adding the new relationship between Book and Author.
                context.SaveChanges();  // Saving the new relationship the user have chosen.
                System.Console.WriteLine($"\nA new instance for BookAuthor is now created!\nA new relationship between Book ID: {bookID} and Author ID: {authorID}.");
                Console.ReadLine();
                return;
            }
        }
    }
}