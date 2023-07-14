using LibraryMidtermData.Model;
using LibraryMidterm.Validation;
using LibraryMidtermData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryMidterm.Services
{
    internal class Inventory
    {  

        public static List<Book> GetCurrentInventory()
        {
            LibraryContext dbContext = new LibraryContext();
            return dbContext.GetBooks();
        }

        public static void SearchByTitle(List<Book> currentInventory)
        {
            Console.Write("Please enter a title: ");
            string titleInput = Console.ReadLine();
            while (Validate.NullCheck(titleInput))
            {
                Console.WriteLine("Please enter a title: ");
                titleInput = Console.ReadLine();
            }

            // Holds the search results based on the user input, prints out a list to the console
            List<Book> searchList = FindTitle(currentInventory, titleInput);

            Console.WriteLine($"\nSearch Results:");
            int count = 0;
            foreach (Book item in searchList)
            {
                Console.WriteLine($"{count + 1}) {item.Title} by {item.Author}");
                count++;
            }
            if (count == 0)
            {
                Console.WriteLine("No search results were found, please try again.");
            }
            Console.WriteLine();
        }

        public static List<Book> FindTitle(List<Book> bookList, string input)
        {
            return (from Book item in bookList
                    where item.Title.ToLower().Contains(input.ToLower())
                    select item).ToList();
        }

        public static void SearchByAuthor(List<Book> currentInventory)
        {
            Console.Write("Please enter an author: ");
            string authorInput = Console.ReadLine();
            while (Validate.NullCheck(authorInput))
            {
                Console.WriteLine("Please enter an author: ");
                authorInput = Console.ReadLine();
            }

            // Holds the search results based on the user input, prints out a list to the console
            List<Book> searchList = FindAuthor(currentInventory, authorInput);

            Console.WriteLine($"\nSearch Results:");
            int count = 0;
            foreach (Book item in searchList)
            {
                Console.WriteLine($"{count + 1}) {item.Title} by {item.Author}");
                count++;
            }
            if (count == 0)
            {
                Console.WriteLine("No search results were found, please try again.");
            }
            Console.WriteLine();
        }

        public static List<Book> FindAuthor(List<Book> bookList, string input)
        {
            return (from Book item in bookList
                    where item.Author.ToLower().Contains(input.ToLower())
                    select item).ToList();

        }

        public static void CurrentCheckedOut(List<Book> currentInventory)
        {
            int count = 1;
            List<Book> outList = new List<Book>();
            Console.WriteLine("Here are the currently checked out books:");

            foreach (var item in from Book item in currentInventory
                                 where item.Status == BookStatusEnum.CheckedOut
                                 select item)
            {
                Console.WriteLine($"{count}) {item.Title} by {item.Author}");
                outList.Add(item);
                count++;
            }

            if (outList.Count == 0)
            {
                Console.WriteLine("There are no books to return, please select a different option.");
            }
            else
            {
                Console.WriteLine("Please select the number of the book you would like to return.");
                int bookPick = int.Parse(Console.ReadLine());
                LibraryTransaction.Return(currentInventory, outList[bookPick - 1]);
            }
        }

        // Pushes the list back to the text file at the end of the current option chosed.
        public static void SaveCurrentInventory(List<Book> currentInventory)
        {
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Users\\jason\\Documents\\repos\\LibraryMidterm\\LibraryMidterm\\Files\\booklist.txt");

                foreach (Book item in currentInventory)
                {
                    sw.WriteLine(Book.BookToCSV(item));
                }

                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to display the MainMenu, allowing user to navigate through the application.
        public static void DisplayCurrentInventory(List<Book> currentInventory)
        {
            Console.WriteLine();
            Console.WriteLine($"Select an option\n1) All Books\n2) Only Available Books");
            string lchoice = Console.ReadLine();

            while (!Validate.Validator(lchoice, @"^[1-2]{1}$"))
            {
                Console.WriteLine("Please enter a valid choice: ");
                lchoice = Console.ReadLine();
            }

            if (int.Parse(lchoice) == 1)
            {
                DisplayAllTitles(currentInventory);
            }
            else
            {
                DisplayAvailableTitles(currentInventory);
            }
            Console.WriteLine();
        }

        private static void DisplayAvailableTitles(List<Book> currentInventory)
        {
            foreach (var item in from Book item in currentInventory
                                 where item.Status == BookStatusEnum.Available
                                 select item)
            {
                Console.WriteLine($"{item.Title,-40} by {item.Author,-25} Available");
            }
        }

        private static void DisplayAllTitles(List<Book> currentInventory)
        {
            foreach (var item in currentInventory)
            {
                if (item.Status == BookStatusEnum.Available)
                {
                    Console.WriteLine($"{item.Title,-40} by {item.Author,-25} Available");
                }
                else
                {
                    Console.WriteLine($"{item.Title,-40} by {item.Author,-25} Unavailable until {item.Date,-20:MM/dd/yyyy}");
                }
            }
        }
    }
}
