using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace Slutuppgift_Bibliotekssystem
{
    public class Author
    {
        public required int ID {get; set;}
        public required string FirstName {get; set;}
        public required string LastName {get; set;}
        public DateOnly BirthYear {get; set;}
        
    }



}
