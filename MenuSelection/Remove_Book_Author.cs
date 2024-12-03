using Slutuppgift_Bibliotekssystem;
using System.Linq;
using System;

public class Remove // Class to delete specific data in the Library (Delete  => CRUD)
{
    public static void Run()
    {
        using (var context = new AppDbContext())
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
                // return;
            }
            
            if (_input == "YES")
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
                        System.Console.WriteLine("Redirecting to Menu. Press any key.");
                        Console.ReadLine();
                        return;
                    default:
                        //  Error handling
                        System.Console.WriteLine("Please select a valid option (1-3). Press any key for Menu.");  
                        Console.ReadLine();                      
                        Remove.Run();
                        return;
                }
            }
            else
            {
                System.Console.WriteLine("Invalid input, you have to enter YES or NO!");
                Console.ReadLine();
                
            }
        }
    }

    private static void RemoveBook()
    {

    }
    private static void RemoveAuthor()
    {

    }

}