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
