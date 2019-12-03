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
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }

            else return false;
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
    }
}
