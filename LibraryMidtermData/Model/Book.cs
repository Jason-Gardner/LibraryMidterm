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
        string Title { get; set; }
        string Author { get; set; }
        string Genre { get; set; }
        int CheckedOut { get; set; }
    }
}
