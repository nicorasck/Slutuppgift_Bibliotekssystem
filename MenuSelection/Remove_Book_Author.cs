using Slutuppgift_Bibliotekssystem;
using System.Linq;
using System;

public class Remove // Class to delete specific data in the Library (Delete  => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                System.Console.WriteLine("REMOVE (Book or Author).");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("YES/NO?");
                Console.ForegroundColor = ConsoleColor.White;
                var _input = Console.ReadLine().ToUpper();
                if (_input == "NO")
                {
                    System.Console.WriteLine("OK, good choice! You will be redirected to the Menu (press any key)");
                    Console.ReadLine();
                    return;
                }
                
                else if (_input == "YES")
                {
                    System.Console.WriteLine("\n1 - Remove a Book.");
                    System.Console.WriteLine("2 - Remove an Author.");
                    System.Console.WriteLine("3 - Go back to the main menu.");

                    var _menuInput = Console.ReadLine();
                    switch (_menuInput)
                    {
                        case "1":
                            RemoveBook();
                            break;
                        case "2":
                            RemoveAuthor();
                            break;
                        case "3":
                            System.Console.WriteLine("Redirecting to main menu. Press any key.");
                            Console.ReadLine();
                            return;
                        default:
                            //  Error handling
                            System.Console.WriteLine("Please select a valid option (1-3). Press any key for Menu.");  
                            Console.ReadLine();                      
                            break;  // The menu will run again.
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid input, you have to enter YES or NO!");
                    Console.ReadLine();       
                }
            }
        }
    }

    private static void RemoveBook()
    {
        using (var context = new AppDbContext())
        {
                var Books = context.Books.ToList(); // Creating a local variable to the list of Books in the library.
                System.Console.WriteLine("Enter a Book ID: ");
                // Error handling if there is no Book with entered ID.
                if (!int.TryParse(Console.ReadLine(), out var bookID))
                {
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    //  Listing all Books with ID to show the user which Book can be erased.
                    foreach (var _book in Books)
                    {
                        System.Console.WriteLine($"Title: {_book.Title} - ID: {_book.BookID}");
                    }
                    Console.ReadLine();
                    return;
                }

                // Check if the book exists in the database
                var removeBook = context.Books.FirstOrDefault(b => b.BookID == bookID);
                if (removeBook == null)
                {
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    Console.ReadLine();
                    return;
                }

                // Showing the matches between Book and Author in BookAuthor table
                var matchedBookAuthor = context.BookAuthors
                    .Where(ba => ba.BookID == bookID)
                    .ToList();
                if (matchedBookAuthor.Any())
                {
                    // If the Book has more than one Author the relationship will be erased as well, based on the BookID.
                    System.Console.WriteLine("Deleting all associations with connected Authors to this Book.");
                }

                context.BookAuthors.RemoveRange(matchedBookAuthor); // Erasing the relationships.
                context.Books.Remove(removeBook);   // Deleting the Book.
                context.SaveChanges();  // Saving changes to the database.
                System.Console.WriteLine($"You've now erased this book: {removeBook.Title}.");
        }
    }
    private static void RemoveAuthor()
    {
        using (var context = new AppDbContext())
        {
                var Authors = context.Authors.ToList(); // Creating a local variable to the list of Authors in the library.
                System.Console.WriteLine("Enter a Book ID: ");
                // Error handling if there is no Author with entered ID.
                if (!int.TryParse(Console.ReadLine(), out var authorID))
                {
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    //  Listing all Authors with ID to show the user which Author can be erased.
                    foreach (var _author in Authors)
                    {
                        System.Console.WriteLine($"Author name: {_author.FirstName} {_author.LastName}");
                    }
                    Console.ReadLine();
                    return;
                }

                // Check if the Author exists in the database
                var removeAuthor = context.Authors.FirstOrDefault(a => a.AuthorID == authorID);
                if (removeAuthor == null)
                {
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    Console.ReadLine();
                    return;
                }

                // Showing the matches between Book and Author in BookAuthor table
                var matchedBookAuthor = context.BookAuthors
                    .Where(ba => ba.AuthorID == authorID)
                    .ToList();
                if (matchedBookAuthor.Any())
                {
                    // If the Book has more than one Author the relationship will be erased as well, based on the BookID.
                    System.Console.WriteLine("Deleting all associations with connected Books to this Author.");
                }

                context.BookAuthors.RemoveRange(matchedBookAuthor); // Erasing the relationships.
                context.Authors.Remove(removeAuthor);   // Deleting the Book.
                context.SaveChanges();  // Saving changes to the database.
                System.Console.WriteLine($"You've now erased this Author: {removeAuthor.FirstName} {removeAuthor.LastName}.");
        }
    }

}