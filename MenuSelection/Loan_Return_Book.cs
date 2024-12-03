using Slutuppgift_Bibliotekssystem;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class LoanBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            const int limit = 2;    // Sets a limit for borrower (can only loan two books at the time). 
            System.Console.WriteLine("These Books are available at the moment:\n");
            var Books = context.Books
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .ToList();

            if (!Books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("There are no Books available, please come back later!");
                Console.ResetColor();
                return;
            }

            foreach (var _book in Books)
            {
                //  Here is the famous JOIN-part where authors and books will be shown from the above implementation.
                var authors = string.Join(", ", _book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}"));
                Console.WriteLine($"Book: {_book.Title, -40} Author: {authors}");
            }
            System.Console.WriteLine("______________________________________________________________\n");

            // Personal data for the Borrower - a registration => will be added to the Borrower Table!
            System.Console.WriteLine("To loan a Book you need to enter some information.\nEnter 'Yes' to proceed or 'No' to cancel.");
            var _input = Console.ReadLine().ToUpper();
            if (_input == "No")
            {
                System.Console.WriteLine("OK - Please press any key for Menu.");
                Console.ReadLine();
                return;
            }
            else if (_input == "Yes")
            {
                System.Console.WriteLine("Enter your First Name: ");
                var _firstName = Console.ReadLine();
                System.Console.WriteLine("Enter your Last Name: ");
                var _lastName = Console.ReadLine();
                System.Console.WriteLine("Enter your Email: ");
                var _email = Console.ReadLine();
                System.Console.WriteLine("Enter your Phone Number");
                var _phoneNumber = Console.ReadLine();
                // Check if the given Borrower do exists in the Data Base or not.
                var _borrower = context.Borrowers
                .FirstOrDefault(b => b.FirstName == _firstName && b.LastName == _lastName && b.Email == _email && b.PhoneNumber == _phoneNumber);

                if (_borrower == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("You are not in the Data Base. A new registration is completed!");
                    Console.ResetColor();
                    
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
                        Thread.Sleep(70); // Pauses for 70 milliseconds => loading letter by letter
                    }
                }
            
            // Checking the limit for the borrower (if there are existing loans => also checking that as well).
            var OngoingLoan = context.Lendings
                .Count(l => l.BorrowerID == _borrower.BorrowerID && !l.IsReturned);

            if (OngoingLoan >= limit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"The loan limit for {_borrower.FirstName} {_borrower.LastName} has reached the limit (max is {limit}!)");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            }
            else
            {
                System.Console.WriteLine("Invalid Input, you will be redirected to the Menu!");
                Console.ReadLine();
                return;
            }
        }
    }
}


public class ReturnBook
{
    public static void Run()
    {
        
    }
}