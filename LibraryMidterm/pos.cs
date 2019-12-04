using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryMidterm
{
    class POS
    {

        //static List of Book objects is used to hold the most current inventory

        public static List<Book> currentInventory = new List<Book>();

        public static void StartBookPOS()
        {
            GetCurrentInventory();
            //SaveCurrentInventory();

            Options();
            bool res = true;
            while (res == true)
            {
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    //Print List
                    DisplayCurrentInventory();
                    Options();

                }

                if (choice == "2")
                {
                    //Search by title
                    SearchByTitle();
                    Options();

                }

                if (choice == "3")
                {
                    // Search by author
                    SearchByAuthor();
                    Options();

                }

                if (choice == "4")
                {
                    // Checkout book
                }
                if (choice == "5")
                {
                    // Return book
                }

                if (choice == "6")
                {
                    Console.WriteLine("Good Bye");
                    res = false;
                }
            }
        }
         
        
        //Get method that will pull in the current inventory from the CSV file
        private static void GetCurrentInventory()
        {
            //first point the StreamReader object at the text file that holds the current inventory in CSV format
            StreamReader sr = new StreamReader(@"C:\Temp\TeamProject1\LibraryMidterm\LibraryMidterm\LibraryMidterm\booklist.txt");

            //string array to hold the split CSV row before parsing into necessary Book object
            string[] csvArray;

            //string that grabs and holds the first line of the CSV text file
            string line = sr.ReadLine();

            //while loop to iterate through the text file of CSV's building inventory List
            while (line != null)//as long as the first line of the text file is not null then continue with parsing
            {
                //spilt the CSV on the comma's until we have the sparate values indexed in our string array
                csvArray = line.Split(',');
                currentInventory.Add(Book.CSVToBook(line));
                line = sr.ReadLine();
            }

            //close the text file when done with File I/O operations
            sr.Close();
        }
        private static void SearchByTitle()
        {
            Console.WriteLine("Please Enter the Title");
            string input = Console.ReadLine();
            var Books = currentInventory.Where(p => p.Title == input).ToList();
            Books.ForEach(p =>
            {
                Console.WriteLine(p.Definition());
            });
        }
        private static void SearchByAuthor()
        {
            Console.WriteLine("Please Enter the Auther Name");
            string input = Console.ReadLine();
            var AuthorName = currentInventory.Where(p => p.Author == input).ToList();
            AuthorName.ForEach(p =>
            {
                Console.WriteLine(p.Definition());
            });

        }
        //public string CheckInOption()
        //{
        //    if (status == true)
        //    {
        //        return "Checked In";
        //    }
        //    return "Checked Out";
        //}
        //method to Save/Update the current inventory
        private static void SaveCurrentInventory()
        {
            try
            {
                //create new streamwriter object
                StreamWriter sw = new StreamWriter(@"C:\Temp\TeamProject1\LibraryMidterm\LibraryMidterm\LibraryMidterm\booklist.txt");

                //iterate through our list of cars and first make CSV string out of the objects data, and then write that data to the CSV text file
                foreach (Book item in currentInventory)
                {
                    sw.WriteLine(Book.BookToCSV((Book)item));
                }

                //closed the connection saving data to the text file
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //method to display the MainMenu to navigate through the application

        //method that will display list of current CarLot inventory
        private static void DisplayCurrentInventory()
        {
            //iterate through the static List of cars
            foreach (Book book in currentInventory)
            {
                //display the information to the user
                Console.WriteLine(book.Definition());
            }
        }

        public static void Options()
        {
            //make a simple menu with options for the User
            Console.WriteLine("Welcome to Our Library!!  Please make a selection from the menu below.");
            Console.WriteLine("1. Print List");
            Console.WriteLine("2. Search by title");
            Console.WriteLine("3. Search by author");
            Console.WriteLine("4. Checkout book");
            Console.WriteLine("5. Return book");
            Console.WriteLine("6. Exit");
        }
    }
}