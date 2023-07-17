using LibraryMidtermData.Model;
using LibraryMidterm.Services;
using LibraryMidterm.Validation;
using System;
using System.Collections.Generic;

namespace LibraryMidterm.UI
{
    class POS
    {
        //Static list we will use to track the movement of books and checkout dates
        public static List<Book> currentInventory = Inventory.GetCurrentInventory();

        public static void StartBookPOS()
        {
            int res = 0;

            do
            {
                OutputUiOptions();
                switch (Validate.OptionInputValidation(Console.ReadLine()))
                {
                    case "1":
                        Inventory.DisplayCurrentInventory(currentInventory);
                        break;
                    case "2":
                        Inventory.SearchByTitle(currentInventory);
                        break;
                    case "3":
                        Inventory.SearchByAuthor(currentInventory);
                        break;
                    case "4":
                        LibraryTransaction.FindAndCheckout(currentInventory);
                        break;
                    case "5":
                        Inventory.CurrentCheckedOut(currentInventory);
                        break;
                    case "6":
                        Console.WriteLine("Thank you for using the Bibliotecha Library System. Goodbye!");
                        Environment.Exit(0);
                        break;
                }
                Inventory.SaveCurrentInventory(currentInventory);
            } while (res < 6);
        }      

        public static void OutputUiOptions()
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