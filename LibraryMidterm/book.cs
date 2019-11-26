using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMidterm
{
    class Book
    {
        private string title;
        private string author;
        private bool checkedOut = false;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public bool CheckedOut
        {
            get { return checkedOut; }
            set { checkedOut = value; }
        }

        public Book()
        {

        }

        public Book(string title, string author, bool checkedOut)
        {

        }

        public void checkOut(Book book)
        {
            
        }
    }
}
