using Slutuppgift_Bibliotekssystem;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace MenuSelection;
public class Remove // Class to delete specific data in the Library (Delete  => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                System.Console.WriteLine("REMOVE (Book / Author / Loan).");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("YES/NO?");
                Console.ResetColor();
                var _input = Console.ReadLine().ToUpper().Trim();
                if (_input == "NO")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    System.Console.WriteLine("OK, good choice! You will be redirected to the Menu (press enter for Main Menu)");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                else if (_input == "YES")
                {
                    System.Console.WriteLine("\n1 - Remove a Book.");
                    System.Console.WriteLine("2 - Remove an Author.");
                    System.Console.WriteLine("3 - Remove a Loan");
                    System.Console.WriteLine("4 - Go back to the Main Menu.");

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
                            RemoveLoan();
                            break;
                        case "4":
                            System.Console.WriteLine("\nRedirecting to Menu for Maintenance.");
                            return;
                        default:
                            //  Error handling
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("\nPlease select a valid option (1-4).");
                            Console.ResetColor();
                            break;  // The menu will run again.
                    }
                }
                else
                {   
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nInvalid input, you have to enter YES or NO!");
                    Console.ResetColor();
                }
            }
        }
    }

    #region RemoveBook
    private static void RemoveBook()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                var Books = context.Books.ToList(); // Creating a local variable to the list of Books in the library.
                System.Console.WriteLine("Enter a Book ID to remove (type 'LIST' to view all books or 'Q' to quit): ");
                var _input = Console.ReadLine()?.Trim();

                // If the user would like to see the books before removing.
                if (_input?.ToUpper() == "LIST")
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    System.Console.WriteLine("List of Books:");
                    Console.ResetColor();
                    foreach (var _book in Books)
                    {
                        System.Console.WriteLine($"Book ID: {_book.BookID,-10} Title: {_book.Title,-30} {_book.Genre}");
                    }
                    continue;
                }

                // If the user would like to exit.
                if (_input?.ToUpper() == "Q")
                {
                    System.Console.WriteLine("Redirecting to Menu for Remove.\n");
                    break;
                }

                // Error handling if there is no Book with entered ID.
                if (!int.TryParse(_input, out var bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                // Check if the book exists in the database
                var removeBook = context.Books.FirstOrDefault(b => b.BookID == bookID);
                if (removeBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    return;
                }

                // Showing the matches between Book and Author in BookAuthor table
                var matchedBookAuthor = context.BookAuthors
                    .Where(ba => ba.BookID == bookID)
                    .ToList();

                if (matchedBookAuthor.Any())
                {
                    // If the Book has more than one Author, the relationship will be erased as well, based on the BookID.
                    System.Console.WriteLine("Deleting all associations with connected Authors to this Book.");
                    context.BookAuthors.RemoveRange(matchedBookAuthor); // Erasing the relationships. Need to do this before deleting the Book (otherwise th FK will go LOCO).
                }

                context.Books.Remove(removeBook);   // Deleting the Book.
                context.SaveChanges();  // Saving changes to the database.
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                System.Console.WriteLine($"You've now erased this book: {removeBook.Title}.\n");
                Console.ResetColor();
                break;
            }
        }
    }
    #endregion

    #region RemoveAuthor
    private static void RemoveAuthor()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                var Authors = context.Authors.ToList(); // Creating a local variable to the list of Authors in the library.
                System.Console.WriteLine("\nEnter an Author ID to remove (type 'LIST' to view all books or 'Q' to quit): ");
                var _input = Console.ReadLine()?.Trim();

                if (_input?.ToUpper() == "LIST")
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    System.Console.WriteLine("List of Authors:");
                    Console.ResetColor();
                    foreach (var _author in Authors)
                    {
                        System.Console.WriteLine($"Author ID: {_author.AuthorID,-10} Name: {_author.FirstName} {_author.LastName}");
                    }
                    continue;
                }

                // If the user would like to exit.
                if (_input?.ToUpper() == "Q")
                {
                    System.Console.WriteLine("Redirecting to Menu for Remove.\n");
                    break;
                }

                // Error handling if there is no Author with entered ID.
                if (!int.TryParse(_input, out var authorID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    return;
                }

                // Check if the Author exists in the database
                var removeAuthor = context.Authors.FirstOrDefault(a => a.AuthorID == authorID);
                if (removeAuthor == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    return;
                }

                // Showing the matches between Book and Author in BookAuthor table
                var matchedBookAuthor = context.BookAuthors
                    .Where(ba => ba.AuthorID == authorID)
                    .ToList();

                if (matchedBookAuthor.Any())
                {
                    // If the Author has more than one Book, the relationship will be erased as well, based on the AuthorID.
                    System.Console.WriteLine("Deleting all associations with connected Books to this Author.");
                    context.BookAuthors.RemoveRange(matchedBookAuthor); // Erasing the relationships. Need to do this before deleting the Author (otherwise th FK will go LOCO).
                    Console.ReadLine();
                }

                context.Authors.Remove(removeAuthor);   // Deleting the Author.
                context.SaveChanges();  // Saving changes to the database.
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                System.Console.WriteLine($"You've now erased this Author: {removeAuthor.FirstName} {removeAuthor.LastName}.\n");
                Console.ResetColor();
                break;
            }
        }
    }
    #endregion

    #region RemoveLoan

    private static void RemoveLoan()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                // JOIN
                var Loan = context.Lendings
                    .Include(l => l.Borrower)   // Details from borrower.
                    .Include(l => l.Book)   // Details from book.
                    .ToList();

                if (!Loan.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There are no loans at the moment.\n");
                    Console.ResetColor();
                    return;
                }

                // Listing the available loans
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Available Loans:");
                Console.ResetColor();
                foreach (var item in Loan)
                {
                    Console.WriteLine($"Loan ID: {item.LoanID,10}, Borrower: {item.Borrower.FirstName} {item.Borrower.LastName,30}, " +
                                      $"Book: {item.Book.Title,20}, Loan Date: {item.LoanDate:yyyy-MM-dd, 15}, Returned: {item.IsReturned}");
                }

                Console.WriteLine("Enter a Loan ID to remove (type 'LIST' to view all books or 'Q' to quit): ");
                var _input = Console.ReadLine()?.Trim();

                // If the user would like to exit.
                if (_input?.ToUpper() == "Q")
                {
                    Console.WriteLine("Redirecting to Menu for Remove.");
                    Console.ReadLine();
                    break;
                }

                // Error handling if there is no Author with entered ID.
                if (!int.TryParse(_input, out var loanId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                var _loan = context.Lendings
                    .Include(l => l.Borrower)   // Details from borrower.
                    .Include(l => l.Book)       // Details from book.
                    .FirstOrDefault(l => l.LoanID == loanId);

                if (_loan == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The ID could not be found, please try again!\n");
                    Console.ResetColor();
                    continue;
                }

                Console.WriteLine($"Are you sure you want to delete Loan ID: {_loan.LoanID,10} (Borrower: {_loan.Borrower.FirstName} {_loan.Borrower.LastName,30}, Book Title: {_loan.Book.Title})? (YES/NO)");
                var _confirmation = Console.ReadLine()?.Trim();

                if (_confirmation?.ToUpper() == "YES")
                {
                    context.Lendings.Remove(_loan); // Removing the loan.
                    context.SaveChanges();          // SAving changes.   
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"You have now removed the loan! ID: {_loan.LoanID}\n");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.WriteLine("Loan deletion canceled.\n");
                    break;
                }
            }
        }
    }

    #endregion
}