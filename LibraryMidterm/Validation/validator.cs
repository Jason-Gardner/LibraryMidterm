using System;
using System.Text.RegularExpressions;

namespace LibraryMidterm.Validation
{
    class Validate
    {
        public static bool Validator(string input, string pattern)
        {
            if (!NullCheck(input))
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

        public static string OptionInputValidation(string input)
        {
            {

                do
                {
                    while (NullCheck(input))
                    {
                        Console.WriteLine("Please enter a valid option.");
                        input = Console.ReadLine();
                    }

                    if (int.Parse(input) > 6 | int.Parse(input) < 1)
                    {
                        Console.WriteLine("Please enter a valid option.");
                        input = Console.ReadLine();
                    }

                } while (!NullCheck(input) && int.Parse(input) > 6);

                return input;


            }
        }
    }
}
