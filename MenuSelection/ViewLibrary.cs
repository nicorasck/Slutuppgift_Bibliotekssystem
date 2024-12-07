using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MenuSelection;
public class ViewLibrary    // Class to read all data in the Library (Read  => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            while (true)
            {
                System.Console.WriteLine("\n1 - View Library.");
                System.Console.WriteLine("2 - View Loan History.");
                System.Console.WriteLine("3 - View Books for an Specific Author.");
                System.Console.WriteLine("4 - View Authors for an Specific Book.");
                System.Console.WriteLine("5 - Go back to the Main Menu.");

                var _menuInput = Console.ReadLine().Trim();
                switch (_menuInput)
                {
                    case "1":
                        ListLibrary.Run();  // bringing the Library to show the user which ID each books and Authors have.
                        break;
                    case "2":
                        LoanHistory.Run();
                        break;
                    case "3":
                        AuthorWithBooks.Run();
                        break;
                    case "4":
                        BooksWithAuthors.Run();
                        break;
                    case "5":
                        System.Console.WriteLine("\nRedirecting to Menu.");
                        return;
                    default:
                        //  Error handling
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("\nPlease select a valid option (1 or 5).");
                        Console.ResetColor();
                        break;  // The menu will run again.
                }
            }
        }
    }
}