using System;
using Microsoft.EntityFrameworkCore;
using Slutuppgift_Bibliotekssystem;

public class Maintenance
{
    public static void Run()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\nMaintenance\n");
        Console.ResetColor();
        int menuSel = 0;
        do
        {
            menuSel = MenuSelection();
            MenuExecution(menuSel);
        }
        while (menuSel != 7);
    }
        private static int MenuSelection()
        {
            int menuSel = 0;
            System.Console.WriteLine("1 - Add an Author.");
            System.Console.WriteLine("2 - Add a Book.");
            System.Console.WriteLine("3 - Update an Author.");
            System.Console.WriteLine("4 - Update a Book Title.");
            System.Console.WriteLine("5 - Add Book to an Author.");
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("6 - REMOVE (Book or Author).");
            Console.ResetColor();
            System.Console.WriteLine("7 - Main Menu.");
            try
            {
                menuSel = Convert.ToInt32(Console.ReadLine());
                if (menuSel < 1 || menuSel > 7)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select a valid option (1-7). Press any key for Menu.");
                    Console.ReadLine();
                    Console.ResetColor();
                    return MenuSelection();
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Please select a valid option (1-7). Press any key for Menu.");
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
                    AddAuthor.Run();
                    break;
                case 2:
                    AddBook.Run();
                    break;
                case 3:
                    UpdateAuthor.Run();
                    break;
                case 4:
                    UpdateBook.Run();
                    break;
                case 5:
                    SetRelationship.Run();
                    break;
                case 6:
                    Remove.Run();
                    break;
                case 7:
                    System.Console.WriteLine("\nYou will be redirected to the Main Menu!");
                    menuSel = 0;
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