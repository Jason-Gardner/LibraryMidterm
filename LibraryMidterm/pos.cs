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

            bool res = true;
            while (res == true)
            {

                Options();
                var choice = Console.ReadLine();
                Validate.OptionInputValidation(choice);
                if (choice == "1")
                {
                    //Print List
                    DisplayCurrentInventory();

                }
                if (choice == "2")
                {
                    //Search by title

                    Console.Write("Please enter a title: ");
                    string titleInput = Console.ReadLine();
                    while (Validate.NullCheck(titleInput))
                    {
                        Console.Write("Please enter a title:");
                        titleInput = Console.ReadLine();
                    }
                    List<Book> searchList = SearchByTitle(currentInventory, titleInput);
                    Console.WriteLine($"\nSearch Results:");
                    int count = 1;
                    foreach (Book item in searchList)
                    {
                        Console.WriteLine($"{count}) {item.Title} by {item.Author}");
                        count++;
                    }
                    Console.WriteLine();

                }
                if (choice == "3")
                {
                    //Search by author
                    Console.Write("Please enter an author: ");
                    string authorInput = Console.ReadLine();
                    while (Validate.NullCheck(authorInput))
                    {
                        Console.Write("Please enter a author: ");
                        authorInput = Console.ReadLine();
                    }

                    List<Book> searchList = SearchByAuthor(currentInventory, authorInput);
                    Console.WriteLine($"\nSearch Results:");
                    int count = 1;
                    foreach (Book item in searchList)
                    {
                        Console.WriteLine($"{count}) {item.Title} by {item.Author}");
                        count++;
                    }
                    Console.WriteLine();
                }


                if (choice == "4")
                {
                    List<Book> searchList = new List<Book>();
                    Console.WriteLine("How would you like to search for your book? Enter a title or author.");
                    string input = Console.ReadLine();
                    while (!Validate.Validator(input.ToLower(), @"^[(author)|(title)]$"))
                    {
                        Console.WriteLine("Please enter a valid input.");
                        input = Console.ReadLine();
                    }
                    if (input == "title")
                    {
                        Console.Write("Please enter a title: ");
                        string titleInput = Console.ReadLine();
                        while (Validate.NullCheck(titleInput))
                        {
                            Console.Write("Please enter a title:");
                            titleInput = Console.ReadLine();
                        }
                        searchList = SearchByTitle(currentInventory, titleInput);
                        Console.WriteLine($"\nSearch Results:");
                        int count = 1;
                        foreach (Book item in searchList)
                        {
                            Console.WriteLine($"{count}) {item.Title} by {item.Author}");
                            count++;
                        }
                        Console.WriteLine();

                        Console.WriteLine("Please select the number of the book you would like to rent.");
                        int bookPick = int.Parse(Console.ReadLine());
                        CheckOut(currentInventory, searchList[bookPick - 1]);
                    }

                    if (input == "author")
                    {
                        Console.Write("Please enter an author ");
                        string search = Console.ReadLine();
                        while (Validate.NullCheck(search))
                        {
                            Console.Write("Please enter a title:");
                            search = Console.ReadLine();
                        }
                        searchList = SearchByTitle(currentInventory, search);
                        Console.WriteLine($"\nSearch Results:");
                        int count = 1;
                        foreach (Book item in searchList)
                        {
                            Console.WriteLine($"{count}) {item.Title} by {item.Author}");
                            count++;
                        }
                        Console.WriteLine();

                        Console.WriteLine("Please select the number of the book you would like to rent.");
                        int bookPick = int.Parse(Console.ReadLine());
                        CheckOut(currentInventory, searchList[bookPick - 1]);
                    }

                }
                if (choice == "5")
                {
                    // Return book
                    int count = 1;
                    List<Book> outList = new List<Book>();
                    Console.WriteLine("Here are the currently checked out books:");
                    foreach (Book item in currentInventory)
                    {
                        if (item.Status == false)
                        {
                            Console.WriteLine($"{count}) {item.Title} by {item.Author}");
                            outList.Add(item);
                            count++;
                        }
                    }

                    Console.WriteLine("Please select the number of the book you would like to rent.");
                    int bookPick = int.Parse(Console.ReadLine());
                    Return(currentInventory, outList[bookPick - 1]);

                }

                if (choice == "6")
                {
                    Console.WriteLine("Thank you for using the Bibliotecha Library System. Goodbye!");
                    res = false;
                }
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

        }

        public static void Return(List<Book> bookList, Book yourBook)
        {
            Console.WriteLine($"You have selected {yourBook.Title}, would you like to return this book?");
            string choice = Console.ReadLine();
            bool yesInputValid = false;
            if (choice == "yes")
            {
                yesInputValid = true;
            }
            if (yesInputValid && !yourBook.Status)
            {
                yourBook.Status = true;
                yourBook.Date = DateTime.Now;
                Console.WriteLine($"You have checked in {yourBook.Title} by {yourBook.Author}.");
                Console.WriteLine("Thank you!");
                SaveCurrentInventory();
            }
            else if (!yourBook.Status)
            {
                Console.WriteLine($"That book is not available until {yourBook.Date.ToString("MM/dd/yyyy")}");
                DisplayCurrentInventory();
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
            Console.WriteLine($"Select an option\n1) All Books\n2) Only Available Books");
            string lchoice = Console.ReadLine();

            while (!Validate.Validator(lchoice, @"^[1-2]{1}$"))
            {
                Console.WriteLine("Please enter a valid choice: ");
                lchoice = Console.ReadLine();
            }

            int invChoice = int.Parse(lchoice);
            if (invChoice == 1)
            {
                foreach (Book item in currentInventory)
                {
                    if (item.Status == true)
                    {
                        Console.WriteLine($"{item.Title,-40} by {item.Author,-25} Available");
                    }
                    if (item.Status == false)
                    {
                        Console.WriteLine($"{item.Title,-40} by {item.Author,-25} Unavailable until {item.Date.ToString("MM/dd/yyyy"),-20}");
                    }
                }
            }
            if (invChoice == 2)
            {
                foreach (Book item in currentInventory)
                {
                    if (item.Status == true)
                    {
                        Console.WriteLine($"{item.Title,-40} by {item.Author,-25} Available");
                    }
                }
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