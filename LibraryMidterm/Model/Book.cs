using System;

namespace LibraryMidterm.Model
{
    // This is the book class, defines our book and gives some definition.

    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public BookStatusEnum Status { get; set; }
        public DateTime Date { get; set; }

        public Book(string title, string author, BookStatusEnum status, DateTime date)
        {
            Title = title;
            Author = author;
            Status = status;
            Date = date;
        }

        public static DateTime CreateDateTime(string input)
        {
            return input == " " ? DateTime.Now : DateTime.Parse(input);
        }

        public string CheckInOption()
        {
            return Status == BookStatusEnum.Available ? "Available" : "Checked Out";
        }
        public virtual string Definition()
        {
            return $"{Title,-40} {Author,-20} {CheckInOption(),-15} {Date.ToShortDateString(),-15}";
        }

        public static Book CSVToBook(string csv)
        {
            string[] bookArray = csv.Split(',');
            return new Book(bookArray[0], bookArray[1], bookArray[2] == "Available" ? BookStatusEnum.Available : BookStatusEnum.CheckedOut, CreateDateTime(bookArray[3]));

        }

        public static string BookToCSV(Book book)
        {
            return $"{book.Title},{book.Author},{book.CheckInOption()},{book.Date}";
        }

    }
}