using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMidterm
{
    class Book
    {
        public string Title;

        public string Author;

        public bool Status;

        public DateTime Date;

        public Book()
        {

        }

        public Book(string title, string author, bool status, DateTime date)
        {
            Title = title;
            Author = author;
            Status = status;
            Date = date;
        }

        public static DateTime createDateTime(string input)
        {
            if (input == " ")
            {
                return DateTime.Now;
            }
            else
            {
                return DateTime.Parse(input);
            }
        }

        public string CheckInOption()
        {
            if (Status == true)
            {
                return "Available";
            }
            return "Checked Out";
        }
        public virtual string Definition()
        {
            return $"{Title,-40} {Author,-20} {CheckInOption(),-15} {Date.ToShortDateString(),-15}";
        }

        public static Book CSVToBook(string csv)
        {
            string[] bookArray = csv.Split(',');
            return new Book(bookArray[0], bookArray[1], bool.Parse(bookArray[2]), createDateTime(bookArray[3]));

        }

        public static string BookToCSV(Book book)
        {
            return $"{book.Title},{book.Author},{book.Status},{book.Date}";
        }

    }
}