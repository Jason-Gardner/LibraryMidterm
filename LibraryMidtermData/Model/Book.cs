using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMidtermData.Model
{
    public class Book
    {
        int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int CheckedOut { get; set; }

        public Book(string title, string author, string genre, BookStatusEnum status)
        {
            Title = title;
            Author = author;
            Genre = genre;
            CheckedOut = (int)status;
        }

        public string CheckInOption()
        {
            return CheckedOut == (int)BookStatusEnum.Available ? "Available" : "Checked Out";
        }

        public static string BookToCSV(Book book)
        {
            return $"{book.Title},{book.Author},{book.CheckInOption()}";
        }
    }
}
