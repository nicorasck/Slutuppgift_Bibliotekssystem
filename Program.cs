using System;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Slutuppgift_Bibliotekssystem;
using MenuSelection;

class Program
{
    static void Main(string[] args)
    {   
        Seed.Run(); // If the existing Seed-Data already have been running before, the user till be notified.    
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
        while (menuSel != 5);
    }
    private static int MenuSelection()
    {
        int menuSel = 0;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Library system - 'Slutuppgift_Bibliotekssystem'");
        Console.ResetColor();
        System.Console.WriteLine("1 - Loan a Book.");
        System.Console.WriteLine("2 - Return a Book.");
        System.Console.WriteLine("3 - View Library (including the Loan History).");
        System.Console.WriteLine("4 - Maintenance.");
        System.Console.WriteLine("5 - EXIT.");
        try
        {
            menuSel = Convert.ToInt32(Console.ReadLine());
            if (menuSel < 1 || menuSel > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select a valid option (1-5). Press any key for Menu.");
                Console.ReadLine();
                Console.ResetColor();
                return MenuSelection();
            }
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Please select a valid option (1-5). Press any key for Menu.");
            Console.ResetColor();
            Console.ReadLine();
            return MenuSelection(); //moving back one step -> into the Menu
        }
        return menuSel;
    }
    private static void MenuExecution(int menuSel)
    {
        switch (menuSel)
        {
            case 1:
                LoanBook.Run();
                break;
            case 2:
                ReturnBook.Run();
                break;
            case 3:
                ViewLibrary.Run();
                break;
            case 4:
                Maintenance.Run();
                break;
            case 5:
                System.Console.WriteLine("Thank you, see you another time!");
                menuSel = 0;    // quitting menu, program will end.
                return;
            default:
                // Error handling
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Eeeey yo, Invalid input! Please try again :)");
                Console.ResetColor();
                Console.ReadLine();
                break;
        }
    }
}
