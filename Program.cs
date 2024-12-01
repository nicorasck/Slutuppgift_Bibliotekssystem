namespace Slutuppgift_Bibliotekssystem;

class Program
{
    static void Main(string[] args)
    {
        DateTime today = DateTime.Now;
        string formattedDate = "";
        formattedDate = string.Format("{0: dddd}", today);
        Console.WriteLine("Hi and welcome to my simple Library System, this is a lovely" + formattedDate +"\n");
        bool bOK = false;

        while (!bOK)
        {
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
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("8 - REMOVE (Book or Author)");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("9 - EXIT");


            var _input = Console.ReadLine();
            switch (_input)
            {
                case "1":

                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "6":

                    break;
                case "7":

                    break;
                default:
                    // Error handling
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Eeeey yo, Invalid input! Please try again :)");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
