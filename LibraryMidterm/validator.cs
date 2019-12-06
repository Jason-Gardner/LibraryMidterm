using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace LibraryMidterm
{
    class Validate
    {
        public static bool Validator(string input, string pattern)
        {
            if (!Validate.NullCheck(input))
            {
                if (Regex.IsMatch(input, pattern))
                {
                    return true;
                } 
            }
            return false;
        }

        public static bool titleInputValidator(string titleInput, string pattern)
        {
            if (Regex.IsMatch(titleInput, pattern))
            {
                return true;
            }
            else if (titleInput == null || titleInput == " " || titleInput == string.Empty)
            {
                Console.WriteLine("Input cannot be empty or null.");
                return false;
            }
            else
            {
                Console.WriteLine("Please enter a valid input of numbers or letters.");
                return false;
            }
        }
        public static bool titleValidator(Book book, string pattern)
        {
            if (Regex.IsMatch(book.Title, pattern))
            {
                return true;
            }
            else
            {
                string test = book.Title.ToLower();
                pattern = pattern.ToLower();
                if (test.Contains(pattern))
                {
                    return true;
                }
                return false;
            }
        }
        public static bool authorInputValidator(string input, string pattern)
        {
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                string test = input.ToLower();
                pattern = pattern.ToLower();
                if (test.Contains(pattern))
                {
                    return true;
                }
                return false;
            }
        }
        public static bool authorValidator(Book book, string pattern)
        {
            if (Regex.IsMatch(book.Author, pattern))
            {
                return true;
            }
            else
            {
                string test = book.Author.ToLower();
                pattern = pattern.ToLower();
                if (test.Contains(pattern))
                {
                    return true;
                }
                return false;
            }
        }
        public static bool NullCheck(string input)
        {
            if (input == null || input == string.Empty || input == " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool OptionInputValidation(string input)
        {
            {
                if (input != "1" && input != "2" && input != "3" && input != "3" && input != "4" && input != "5" && input != "6")
                {
                    Console.WriteLine("Enter a valid input please");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
