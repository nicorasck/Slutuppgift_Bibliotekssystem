using System;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Slutuppgift_Bibliotekssystem;


class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("TEST");
        
        DateTime today = DateTime.Now;
        string formattedDate = "";
        formattedDate = string.Format("{0: dddd}", today);
        Console.WriteLine("Hi and welcome to my simple Library System, this is a lovely" + formattedDate + "\n");
        int menuSel = 0;
        do
        {
            menuSel = MenuSelection();
            MenuExecution(menuSel);
        }
        while (menuSel != 10);
    }
    private static int MenuSelection()
    {
        int menuSel = 0;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Library system - 'Slutuppgift_Bibliotekssystem'\n");
        Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine("1 - Add Author.");
        System.Console.WriteLine("2 - Add Book.");
        System.Console.WriteLine("3 - Loan a Book.");
        System.Console.WriteLine("4 - Return a Book.");
        System.Console.WriteLine("5 - View Library.");
        System.Console.WriteLine("6 - Update a Book.");
        System.Console.WriteLine("7 - Update an Author.");
        System.Console.WriteLine("8 - Add Book to an Author");
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("9 - REMOVE (Book or Author)");
        Console.ForegroundColor = ConsoleColor.White;
        System.Console.WriteLine("10 - EXIT");
        try
        {
            menuSel = Convert.ToInt32(Console.ReadLine());
            if (menuSel < 1 || menuSel > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select a valid option (1-9). Press any key for Menu.");
                Console.ForegroundColor = ConsoleColor.White;
                return MenuSelection();
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Please select a valid option (1-9). Press any key for Menu.");
            Console.ForegroundColor = ConsoleColor.White;
            return MenuSelection(); //moving back one step -> into the Menu
        }
        return menuSel;
    }
    private static void MenuExecution(int menuSel)
    {
        switch (menuSel)
        {
            case 1:
                AddBook.Run();
                break;
            case 2:
                AddAuthor.Run();
                break;
            case 3:
                LoanBook.Run();
                break;
            case 4:
                ReturnBook.Run();
                break;
            case 5:
                ViewLibrary.Run();
                break;
            case 6:
                UpdateBook.Run();
                break;
            case 7:
                UpdateAuthor.Run();
                break;
            case 8:
                SetRelationship.Run();
                break;
            case 9:
                RemoveBookAuthor.Run();
                break;
            case 10:
                System.Console.WriteLine("Thank you, see you another time!");
                menuSel = 0;    // quitting menu, program will end.
                return;
            default:
                // Error handling
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Eeeey yo, Invalid input! Please try again :)");
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }
}
