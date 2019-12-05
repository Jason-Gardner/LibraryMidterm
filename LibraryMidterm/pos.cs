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

                }

                if (choice == "2")
                {
                    //Search by title
                    Console.Write("Please enter a title: ");
                    string search = Console.ReadLine();
                    List<Book> searchList = SearchByTitle(currentInventory, search);
                    foreach (Book item in searchList)
                    {
                        Console.WriteLine($"{item.Title} {item.Author}");
                    }

                }

                if (choice == "3")
                {
                    // Search by author
                    Console.Write("Please enter an author: ");
                    string search = Console.ReadLine();
                    List<Book> searchList = SearchByAuthor(currentInventory, search);
                    foreach (Book item in searchList)
                    {
                        Console.WriteLine($"{item.Title} {item.Author}");
                    }

                }

                if (choice == "4")
                {
                    List<Book> searchList = new List<Book>();
                    Console.WriteLine("How would you like to search for your book?");
                    string input = Console.ReadLine();
                    if (input == "title")
                    {
                        Console.Write("Please enter a title: ");
                        string search = Console.ReadLine();
                        searchList = SearchByTitle(currentInventory, search);
                        foreach (Book item in searchList)
                        {
                            Console.WriteLine($"{item.Title} {item.Author}");
                        }
                    }
                    if (input == "author")
                    {
                        Console.Write("Please enter an author: ");
                        string search = Console.ReadLine();
                        searchList = SearchByAuthor(currentInventory, search);
                        foreach (Book item in searchList)
                        {
                            Console.WriteLine($"{item.Title} {item.Author}");
                        }
                    }

                    Console.WriteLine("Please select the number of the book you would like to rent.");
                    int bookPick = int.Parse(Console.ReadLine());
                    foreach (Book selection in searchList)
                    {
                        foreach (Book item in currentInventory)
                        {
                            if (item.Title == selection.Title && item.Author == selection.Author)
                            {
                                CheckOut(currentInventory, item);
                            }
                        }
                    }
                }
                if (choice == "5")
                {
                    // Return book
                    Return();
                }

                if (choice == "6")
                {
                    Console.WriteLine("Good Bye");
                    res = false;
                }

                Options();
                SaveCurrentInventory();
            }
        }

        public static void CheckOut(List<Book> bookList, Book yourBook)
        {
            Console.WriteLine($"You have selected {yourBook.Title}, would you like to rent this book?");
            string choice = Console.ReadLine();
            bool yesInputValid = false;
            if (choice == "yes")
            {
                yesInputValid = true;
            }
            if (yesInputValid && yourBook.Status)
            {
                yourBook.Status = false;
                yourBook.Date = (DateTime.Now.AddDays(14));
                Console.WriteLine($"You have checked out {yourBook.Title} by {yourBook.Author}.");
                Console.WriteLine($"It is due on {yourBook.Date.ToString("MM/dd/yyyy")}.");
                SaveCurrentInventory();
                DisplayCurrentInventory();
            }
            else if (!yourBook.Status)
            {
                Console.WriteLine($"That book is not available until {yourBook.Date.ToString("MM/dd/yyyy")}");
                DisplayCurrentInventory();
            }
            else
            {
                DisplayCurrentInventory();
            }
        }

        public static string ReturnBook(Book yourBook)
        {
            if (yourBook.Status == true)
            {
                yourBook.Status = false;
                yourBook.Status = true;
                SaveCurrentInventory();
                return "Thank you for returning your book!";
            }
            return "Book cannot be found";
        }

        public static void Return()
        {
            Console.WriteLine(" Please enter the title of the book");
            String input = Console.ReadLine();
            foreach (Book item in currentInventory.Where(w => w.Title == input))
            {
                item.Status = true;
                item.Date = DateTime.Now;
            }
        }


        //Get method that will pull in the current inventory from the CSV file
        private static void GetCurrentInventory()
        {
            //first point the StreamReader object at the text file that holds the current inventory in CSV format
            StreamReader sr = new StreamReader(@"C:\Users\Jason Gardner\Documents\Projects\LibraryMidterm\LibraryMidterm\booklist.txt");

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
        private static List<Book> SearchByTitle(List<Book> bookList, string input)
        {
            List<Book> tempList = new List<Book>();
            foreach (Book item in bookList)
            {
                if (item.Title.ToLower().Contains(input.ToLower()))
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }

        private static List<Book> SearchByAuthor(List<Book> bookList, string input)
        {
            List<Book> tempList = new List<Book>();
            foreach (Book item in bookList)
            {
                if (item.Author.ToLower().Contains(input.ToLower()))
                {
                    tempList.Add(item);
                }
            }
            return tempList;

        }
        
        public string CheckInOption()
        {
            return "";
        }
        
        private static void SaveCurrentInventory()
        {
            try
            {
                //create new streamwriter object
                StreamWriter sw = new StreamWriter(@"C:\Users\Jason Gardner\Documents\Projects\LibraryMidterm\LibraryMidterm\booklist.txt");

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

        private static void DisplayCurrentInventory()
        {
            Console.WriteLine();
            foreach (Book book in currentInventory)
            {
                //display the information to the user
                Console.WriteLine(book.Definition());
            }
            Console.WriteLine();
        }

        public static void Options()
        {
            //make a simple menu with options for the User
            Console.WriteLine("**********************************************************************");
            Console.WriteLine("Welcome to the Bibliotecha!!  Please make a selection from the menu below.");
            Console.WriteLine("1. Print List");
            Console.WriteLine("2. Search by title");
            Console.WriteLine("3. Search by author");
            Console.WriteLine("4. Checkout book");
            Console.WriteLine("5. Return book");
            Console.WriteLine($"6. Exit");
            Console.WriteLine("**********************************************************************");
        }
    }
}