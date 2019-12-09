using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryMidterm
{
    class POS
    {
        //Static list we will use to track the movement of books and checkout dates

        public static List<Book> currentInventory = new List<Book>();

        public static void StartBookPOS()
        {
            // Builds the current book inventory from a text file using streamreader
            GetCurrentInventory();

            bool res = true;
            while (res == true)
            {
                // Initializes the POS for the user and gives them visual options to select what they want to do
                Options();
                var choice = Validate.OptionInputValidation(Console.ReadLine());

                

                if (choice == "1")
                {
                    //Prints the current list of books, allowing you to select between all books and available books
                    DisplayCurrentInventory();

                }
                if (choice == "2")
                {
                    //Allows the user to search the library by title

                    Console.Write("Please enter a title: ");
                    string titleInput = Console.ReadLine();
                    while (Validate.NullCheck(titleInput))
                    {
                        Console.WriteLine("Please enter a title: ");
                        titleInput = Console.ReadLine();
                    }

                    // Holds the search results based on the user input, prints out a list to the console
                    List<Book> searchList = SearchByTitle(currentInventory, titleInput);

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
                if (choice == "3")
                {
                    //Allows the user to search the library by author
                    Console.Write("Please enter an author: ");
                    string authorInput = Console.ReadLine();
                    while (Validate.NullCheck(authorInput))
                    {
                        Console.WriteLine("Please enter an author: ");
                        authorInput = Console.ReadLine();
                    }

                    // Holds the search results based on the user input, prints out a list to the console
                    List<Book> searchList = SearchByAuthor(currentInventory, authorInput);

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


                if (choice == "4")
                {
                    //This is the area where the user searches for the book they want to checkout and calls a method
                    // to manipulate the data within the list to change the book to being checked out

                    List<Book> searchList = new List<Book>();
                    Console.WriteLine("How would you like to search for your book? Enter a 'Title' or 'Author'.");
                    string input = Console.ReadLine();
                    do
                    {
                        if (Validate.NullCheck(input))
                        {
                            Console.WriteLine("Please enter a 'Title' or 'Author': ");
                            input = Console.ReadLine();
                        }

                        if (!Validate.Validator(input.ToLower(), @"\b(author|title)\b"))
                        {
                            Console.WriteLine("Please enter 'Title' or 'Author': ");
                            input = Console.ReadLine();
                        }

                    } while (input.ToLower() != "title" && input.ToLower() != "author");

                    if (input == "title")
                    {
                        Console.Write("Please enter a title: ");
                        string titleInput = Console.ReadLine();
                        while (Validate.NullCheck(titleInput))
                        {
                            Console.WriteLine("Please enter a title:");
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
                        if (count == 0)
                        {
                            Console.WriteLine("No search results were found, please try again.");
                        }
                        if (count >= 1)
                        {
                            Console.Write("Please enter the number of the book you would like to check out: ");
                            string bookPick = Console.ReadLine();
                            int checkBook;

                            do
                            {
                                if (Validate.NullCheck(bookPick))
                                {
                                    Console.WriteLine("Please enter a valid input.");
                                    bookPick = Console.ReadLine();
                                }

                                if (int.TryParse(bookPick, out checkBook))
                                {
                                    checkBook = int.Parse(bookPick);
                                    if (checkBook >= searchList.Count)
                                    {
                                        Console.WriteLine("That number does not exist, please enter a number above.");
                                        bookPick = Console.ReadLine();
                                    }
                                }

                            } while (!int.TryParse(bookPick, out checkBook) | checkBook > searchList.Count);
                            CheckOut(currentInventory, searchList[checkBook - 1]);
                        }
                    }

                    if (input == "author")
                    {
                        Console.Write("Please enter an author: ");
                        string search = Console.ReadLine();
                        while (Validate.NullCheck(search))
                        {
                            Console.Write("Please enter an author:");
                            search = Console.ReadLine();
                        }
                        searchList = SearchByAuthor(currentInventory, search);

                        Console.WriteLine($"\nSearch Results:");
                        int count = 0;
                        foreach (Book item in searchList)
                        {
                            Console.WriteLine($"{count + 1}) {item.Title} by {item.Author}");
                            count++;
                        }
                        Console.WriteLine();

                        if (count == 0)
                        {
                            Console.WriteLine("No search results were found, please try again.");
                        }
                        if (count >= 1)
                        {
                            Console.WriteLine("Please enter the number of the book you would like to check out.");
                            string bookPick = Console.ReadLine();
                            int checkBook;

                            do
                            {
                                if (Validate.NullCheck(bookPick))
                                {
                                    Console.WriteLine("Please enter a valid input.");
                                    bookPick = Console.ReadLine();
                                }

                                if (int.TryParse(bookPick, out checkBook))
                                {
                                    checkBook = int.Parse(bookPick);
                                    if (checkBook >= searchList.Count)
                                    {
                                        Console.WriteLine("That number does not exist, please enter a number above.");
                                        bookPick = Console.ReadLine();
                                    }
                                }

                            } while (!int.TryParse(bookPick, out checkBook) | checkBook > searchList.Count);

                            CheckOut(currentInventory, searchList[checkBook - 1]);
                        }
                    }

                }
                if (choice == "5")
                {
                    // Displays the list of returnable books and allows the user to select one based on what is found in the list
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

                    Console.WriteLine("Please select the number of the book you would like to return.");
                    int bookPick = int.Parse(Console.ReadLine());
                    Return(currentInventory, outList[bookPick - 1]);

                }

                if (choice == "6")
                {
                    // Presents the user with an ending message upon exiting the program
                    Console.WriteLine("Thank you for using the Bibliotecha Library System. Goodbye!");
                    res = false;
                }
                SaveCurrentInventory();
            }
        }


        // These are the methods that manipulate the data in the background, including search, checkout and return.

        public static void CheckOut(List<Book> bookList, Book yourBook)
        {
            Console.WriteLine($"You have selected {yourBook.Title}, would you like to rent this book?");
            string choice = Console.ReadLine();
            do
            {
                if (Validate.NullCheck(choice))
                {
                    Console.WriteLine("Please enter a valid input.");
                    choice = Console.ReadLine();
                }

                if (!Validate.Validator(choice.ToLower(), @"\b(yes|no)\b"))
                {
                    Console.WriteLine("Please enter a valid input.");
                    choice = Console.ReadLine();
                }

            } while (choice.ToLower() != "yes" && choice.ToLower() != "no");

            if (choice.ToLower() == "yes" && yourBook.Status)
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
            do
            {
                if (Validate.NullCheck(choice))
                {
                    Console.WriteLine("Please enter a valid input.");
                    choice = Console.ReadLine();
                }

                if (!Validate.Validator(choice.ToLower(), @"\b(yes|no)\b"))
                {
                    Console.WriteLine("Please enter a valid input.");
                    choice = Console.ReadLine();
                }

            } while (choice.ToLower() != "yes" && choice.ToLower() != "no");


            if (choice.ToLower() == "yes" && !yourBook.Status)
            {
                yourBook.Status = true;
                yourBook.Date = DateTime.Now;
                Console.WriteLine($"You have checked in {yourBook.Title} by {yourBook.Author}.");
                Console.WriteLine("Thank you!");
                SaveCurrentInventory();
            }

            if (choice.ToLower() == "yes" && !yourBook.Status)
            {
                Console.WriteLine($"That book is not available until {yourBook.Date.ToString("MM/dd/yyyy")}");
                DisplayCurrentInventory();
            }
        }


        //This method will pull in the current inventory from the CSV file and store it to a list.
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

        // Search method, adds hits to a list and returns the list for viewing to the user.
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

        // Pushes the list back to the text file at the end of the current option chosed.
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

        //Method to display the MainMenu, allowing user to navigate through the application.

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
            //Makes a simple menu with options for the User.

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