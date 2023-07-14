using LibraryMidterm.Model;
using LibraryMidterm.Validation;
using System;
using System.Collections.Generic;

namespace LibraryMidterm.Services
{
    internal class LibraryTransaction
    {
        public LibraryTransaction() { }

        public static void ConfirmCheckout(List<Book> bookList, Book yourBook)
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

            if (choice.ToLower() == "yes" && yourBook.Status == BookStatusEnum.Available)
            {
                yourBook.Status = BookStatusEnum.CheckedOut;
                yourBook.Date = DateTime.Now.AddDays(14);
                Console.WriteLine($"You have checked out {yourBook.Title} by {yourBook.Author}.");
                Console.WriteLine($"It is due on {yourBook.Date.ToString("MM/dd/yyyy")}.");
                Inventory.SaveCurrentInventory(bookList);
            }
        }

        public static void FindAndCheckout(List<Book> currentInventory)
        {
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
                searchList = Inventory.FindTitle(currentInventory, titleInput);
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

                    if (Validate.NullCheck(bookPick))
                    {
                        Console.WriteLine("Please enter a valid input.");
                        bookPick = Console.ReadLine();
                    }

                    if (int.TryParse(bookPick, out checkBook))
                    {
                        checkBook = int.Parse(bookPick);
                        if (checkBook > searchList.Count)
                        {
                            Console.WriteLine("That number does not exist, please enter a number above.");
                            bookPick = Console.ReadLine();
                        }
                    }
                    LibraryTransaction.ConfirmCheckout(currentInventory, searchList[checkBook - 1]);
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
                searchList = Inventory.FindAuthor(currentInventory, search);

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

                    if (Validate.NullCheck(bookPick))
                    {
                        Console.WriteLine("Please enter a valid input.");
                        bookPick = Console.ReadLine();
                    }

                    if (int.TryParse(bookPick, out checkBook))
                    {
                        checkBook = int.Parse(bookPick);
                        if (checkBook > searchList.Count)
                        {
                            Console.WriteLine("That number does not exist, please enter a number above.");
                            bookPick = Console.ReadLine();
                        }
                    }
                    LibraryTransaction.ConfirmCheckout(currentInventory, searchList[checkBook - 1]);
                }
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


            if (choice.ToLower() == "yes" && yourBook.Status == BookStatusEnum.CheckedOut)
            {
                yourBook.Status = BookStatusEnum.Available;
                yourBook.Date = DateTime.Now;
                Console.WriteLine($"You have checked in {yourBook.Title} by {yourBook.Author}.");
                Console.WriteLine("Thank you!");
                Inventory.SaveCurrentInventory(bookList);
            }

            if (choice.ToLower() == "yes" && yourBook.Status == BookStatusEnum.CheckedOut)
            {
                Console.WriteLine($"That book is not available until {yourBook.Date.ToString("MM/dd/yyyy")}");
                Inventory.DisplayCurrentInventory(bookList);
            }
        }
    }
}
