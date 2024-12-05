using Slutuppgift_Bibliotekssystem;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

#region LoanBook
public class LoanBook   // Class to Loan a book and to add data for a Borrower.
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                const int limit = 2;    // Sets a limit for borrower (can only loan two books at the time). 
                System.Console.WriteLine("\nThese Books are available at the moment:\n");
                var Books = context.Books
                    .Include(ba => ba.BookAuthors)
                    .ThenInclude(a => a.Author)
                    .Include(b => b.Lendings)
                    .ToList();

                if (!Books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\nThere are no Books available, please come back later!\n");
                    Console.ResetColor();
                    Console.ReadLine();
                    break;
                }

                foreach (var _book in Books)
                {
                    //  Here is the famous JOIN-part where authors and books will be shown from the above implementation.
                    var authors = string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                    // Looking for the last status concerning the loan for the book.
                    var bookStatus = context.Lendings
                    .Any(l => l.BookID == _book.BookID && !l.IsReturned);
                    
                    Console.WriteLine($"Book ID: {_book.BookID,-5} Title: {_book.Title,-40} Author: {authors, -25} Is out for loan: {(bookStatus ? "Yes" : "No")}");
                }
                System.Console.WriteLine("\nPress any key to continue.");
                Console.ReadLine();
                System.Console.WriteLine("______________________________________________________________\n");

                // Personal data for the Borrower - a registration => will be added to the Borrower Table!
                System.Console.WriteLine("To loan a Book, enter 'Yes' to proceed or 'No' to cancel.");
                var _input = Console.ReadLine().ToUpper();
                if (_input == "NO")
                {
                    System.Console.WriteLine("Mission aborted - Please press any key for Menu.");
                    Console.ReadLine();
                    return;
                }
                else if (_input != "YES")
                {
                    System.Console.WriteLine("Invalid Input, you will be redirected to the Menu!");
                    Console.ReadLine();
                    return;
                }

                // Letting the User enter First- and last name => to find out if the user already exists in the Data Base or not.
                System.Console.WriteLine("Enter your First Name: ");
                var _firstName = Console.ReadLine()?.Trim();
                System.Console.WriteLine("Enter your Last Name: ");
                var _lastName = Console.ReadLine()?.Trim();

                // Error handling (the names cannot be empty).
                if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("First Name and Last Name cannot be empty.");
                    Console.ResetColor();
                    continue;
                }

                // Check if the given Borrower do exists in the Data Base or not.
                var _borrower = context.Borrowers
                .FirstOrDefault(b => b.FirstName == _firstName && b.LastName == _lastName); // Not necessary to look for the all properties in Class Borrower!

                if (_borrower == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("You are not in the Data Base. A new registration will be proceeded.");
                    Console.ResetColor();

                    System.Console.WriteLine("Enter your Email: ");
                    var _email = Console.ReadLine()?.Trim();
                    System.Console.WriteLine("Enter your Phone Number");
                    var _phoneNumber = Console.ReadLine()?.Trim();

                    //  Creating a new instance for a Borrower (required properties).
                    _borrower = new Borrower
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        Email = _email,
                        PhoneNumber = _phoneNumber
                    };
                    context.Borrowers.Add(_borrower);   // Adding the borrower.
                    context.SaveChanges();  // Saving the new instance.
                    string text = $"Borrower '{_firstName} {_lastName}' has been registered...!\n";
                    foreach (char t in text)
                    {
                        Console.Write(t);
                        Thread.Sleep(100); // Pauses for 100 milliseconds => loading letter by letter
                    }
                }

                // Checking the limit for the borrower (if there are existing loans => also checking that as well).
                var OngoingLoan = context.Lendings
                    .Count(l => l.BorrowerID == _borrower.BorrowerID && !l.IsReturned);

                if (OngoingLoan >= limit) // limit is set with a constant => 2;
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"The loan limit for {_borrower.FirstName} {_borrower.LastName} has reached the limit (max is {limit}!)");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                // Getting the BookID from the user
                System.Console.WriteLine("Enter the ID for the Book you would like to loan: ");
                if (!int.TryParse(Console.ReadLine(), out var bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    Console.ResetColor();
                    continue;
                }

                var loanBook = context.Books.Find(bookID);
                if (loanBook == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Book not found. Please check the Book ID and try again.");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;   // User will be redirected to the List of Books
                }

                //  Error handling.
                if (!loanBook.IsAvailable) // Checking if the Book is available or not.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The selected book is currently unavailable.");
                    Console.ResetColor();
                    continue;
                }

                // Creating a new instance in Lending.
                var _period = 10; // Sets the loan period for 10 days
                var _loan = new Lending
                {
                    BookID = bookID,
                    BorrowerID = _borrower.BorrowerID, // Reference BorrowerID
                    LoanDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(_period),
                    IsReturned = false
                };

                context.Lendings.Add(_loan);    // Adding loan.
                loanBook.IsAvailable = false;   // Book is now unavailable.
                context.SaveChanges();          // Saving changes (loan).

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                System.Console.WriteLine("\nConfirmation:\n");
                Console.ResetColor();
                System.Console.WriteLine($"Borrower: {_borrower.FirstName,-30} {_borrower.LastName}");
                System.Console.WriteLine($"Book: {loanBook.Title,-30}");
                System.Console.WriteLine($"ID: {bookID,-30}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"\nPlease return the book by {DateTime.Now.AddDays(_period):yyyy-MM-dd}.");
                Console.ResetColor();
                Console.ReadLine();
                break;
            }
        }
    }
}
#endregion

#region ReturnBook
public class ReturnBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                System.Console.WriteLine("Please enter the First Name of the borrower: ");
                var _boFirstName = Console.ReadLine();
                System.Console.WriteLine("Please enter the Last Name of the borrower: ");
                var _boLastName = Console.ReadLine();

                System.Console.WriteLine($"Hello {_boFirstName} {_boLastName}, please enter the Book ID: ");
                if (!int.TryParse(Console.ReadLine(), out var bookID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The ID could not be found, please try again!");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                // Checking if the given name combination does exists in the entity for Borrower.
                var _borrower = context.Borrowers
                    .FirstOrDefault(b => b.FirstName == _boFirstName && b.LastName == _boLastName);

                if (_borrower == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("The name of the borrower cannot be found in the Data Base!");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }
#region OverDue
                // A code for an overdue would suit here (?)
#endregion
                // Checking if there are any ongoing loans => if there are any connections between the borrower and book(s).
                var _loan = context.Lendings
                    .FirstOrDefault(l => l.BorrowerID == _borrower.BorrowerID && l.BookID == bookID && !l.IsReturned);

                if (_loan == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"Cannot find any ongoing loan for {_boFirstName} {_boLastName} with this book!");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                _loan.IsReturned = true; // If match, the book will be marked as returned.
                _loan.ReturnDate = DateTime.Now; // Register the return date.

                var _book = context.Books.Find(bookID); // Sets the new status for the Book bc it is returned.
                if (_book != null)
                {
                    _book.IsAvailable = true;
                }
                context.SaveChanges();  // Saving the new changes/status.
                System.Console.WriteLine($"Thank you {_boFirstName} {_boLastName}, the Book is now returned (ID: {bookID}).");
                Console.ForegroundColor = ConsoleColor.Gray;
                System.Console.WriteLine("(Press any key for Menu)");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
        }
    }
}
#endregion