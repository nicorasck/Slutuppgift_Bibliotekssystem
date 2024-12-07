using System;
using Slutuppgift_Bibliotekssystem;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;

// This Seed class is just for adding Data (to be existing when running the program)
public class Seed
{
        public static void Run()
        {
                using (var context = new AppDbContext())
                {       // Start transaction
                        var transaction = context.Database.BeginTransaction();

                        try
                        {
                                // Checking if the values down below do already exists or not, in this case they do exists!
                                if (!context.Books.Any() && !context.Authors.Any() && !context.BookAuthors.Any())
                                {
                                        var book1 = new Book
                                        {
                                                Title = "To Kill a Mockingbird",
                                                Genre = "Fiction",
                                                Publisher = "J.B. Lippincott & Co.",
                                                ReleaseDate = new DateOnly(1960, 7, 11),    // adding manually which date the book was released
                                                IsAvailable = true
                                        };

                                        var book2 = new Book
                                        {
                                                Title = "1984",
                                                Genre = "Dystopian",
                                                Publisher = "Secker & Warburg",
                                                ReleaseDate = new DateOnly(1949, 6, 8),
                                                IsAvailable = true
                                        };

                                        var book3 = new Book
                                        {
                                                Title = "Pride and Prejudice",
                                                Genre = "Romance",
                                                Publisher = "T. Egerton, Whitehall",
                                                ReleaseDate = new DateOnly(1813, 1, 28),
                                                IsAvailable = true
                                        };

                                        var book4 = new Book
                                        {
                                                Title = "The Catcher in the Rye",
                                                Genre = "Fiction",
                                                Publisher = "Little, Brown and Company",
                                                ReleaseDate = new DateOnly(1951, 7, 16),
                                                IsAvailable = true
                                        };

                                        var book5 = new Book
                                        {
                                                Title = "The Great Gatsby",
                                                Genre = "Tragedy",
                                                Publisher = "Charles Scribner's Sons",
                                                ReleaseDate = new DateOnly(1925, 4, 10),
                                                IsAvailable = true
                                        };


                                        var author1 = new Author
                                        {
                                                FirstName = "Harper",
                                                LastName = "Lee",
                                                BirthYear = 1926
                                        };

                                        var author2 = new Author
                                        {
                                                FirstName = "George",
                                                LastName = "Orwell",
                                                BirthYear = 1903
                                        };

                                        var author3 = new Author
                                        {
                                                FirstName = "Jane",
                                                LastName = "Austen",
                                                BirthYear = 1775
                                        };

                                        var author4 = new Author
                                        {
                                                FirstName = "J.D.",
                                                LastName = "Salinger",
                                                BirthYear = 1919
                                        };

                                        var author5 = new Author
                                        {
                                                FirstName = "F. Scott",
                                                LastName = "Fitzgerald",
                                                BirthYear = 1896
                                        };


                                        //  you can use AddRange() to minimize the code (this time I won't bc I asked ChatGPT how to 'overload', to shorten this code)
                                        context.Books.Add(book1);
                                        context.Books.Add(book2);
                                        context.Books.Add(book3);
                                        context.Books.Add(book4);
                                        context.Books.Add(book5);

                                        context.Authors.Add(author1);
                                        context.Authors.Add(author2);
                                        context.Authors.Add(author3);
                                        context.Authors.Add(author4);
                                        context.Authors.Add(author5);

                                        context.SaveChanges();

                                        //  This is necessary to establish the relationships! Using an Array this time.
                                        var bookAuthors = new[]
                                        {
                                                new BookAuthor {Book = book1, Author = author1},
                                                new BookAuthor {Book = book2, Author = author2},
                                                new BookAuthor {Book = book3, Author = author3},
                                                new BookAuthor {Book = book4, Author = author4},
                                                new BookAuthor {Book = book5, Author = author5}
                                        };

                                        context.BookAuthors.AddRange(bookAuthors);  // Relationships in this context will be added.
                                        context.SaveChanges();  // Need to save the added relationships in the context.
                                        transaction.Commit();

                                        //  Creating a new instance for a Borrower (required properties).
                                        var _borrower = new Borrower
                                        {
                                                FirstName = "Joar",
                                                LastName = "Monell",
                                                Email = "Jojje@boy.com",
                                                PhoneNumber = "0734457354"
                                        };
                                        context.Borrowers.AddRange(_borrower);   // Adding the borrower.
                                        context.SaveChanges();

                                        var _loan = new Lending
                                        {
                                        BookID = 1,
                                        BorrowerID = 10, // Reference BorrowerID
                                        LoanDate = DateTime.Now,
                                        ReturnDate = DateTime.Now.AddDays(10),
                                        IsReturned = false
                                        };
                                        context.Lendings.AddRange(_loan); // Adding the loaner.
                                        context.SaveChanges();
                                
                                        System.Console.WriteLine("Saved changes");
                                }
                                else
                                {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        System.Console.WriteLine("Already inserted.");
                                        Console.ResetColor();
                                }
                        }
                        catch (Exception ex)    // took this part from Mr. Aladdin, can't invent the wheel all the time. TY Bruh! :)
                        {
                                transaction.Rollback();
                                System.Console.WriteLine("Ops! An error occurred: " + ex.Message);
                        }

                }
        }

}