using System;
using Slutuppgift_Bibliotekssystem;

namespace MenuSelection;
public class SetRelationship
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                System.Console.WriteLine("\n- - - - - - - Library - - - - - - -\n");
                Console.ResetColor();
                ListLibrary.Run();  // bringing the Library to show the user which ID each books and Authors have.
                System.Console.WriteLine();

                //  Instructions for the user.
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine("To establish a relationship between a Book and an Author you need to enter the specific ID's for each one.");
                Console.ResetColor();
                System.Console.Write("1. Enter a Book ID: ");
                //  Error handling
                if (!int.TryParse(Console.ReadLine(), out int bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!");
                    Console.ResetColor();
                    continue;
                }

                System.Console.Write("2. Enter an Author ID: ");
                //  Error handling
                if (!int.TryParse(Console.ReadLine(), out int authorID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThe ID could not be found, please try again!");
                    Console.ResetColor();
                    continue;
                }

                //  creating a new instance for the new relationship between Book and Author.
                var bookAuthor = new BookAuthor {BookID = bookID, AuthorID = authorID};

                context.BookAuthors.Add(bookAuthor);    // Adding the new relationship between Book and Author.
                context.SaveChanges();  // Saving the new relationship the user have chosen.
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                System.Console.WriteLine($"\nA new instance for BookAuthor is now created!\nA new relationship between Book ID: {bookID} and Author ID: {authorID}.\n");
                Console.ResetColor();
                return;
            }
        }
    }
}