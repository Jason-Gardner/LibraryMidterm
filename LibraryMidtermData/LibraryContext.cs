using Dapper;
using LibraryMidtermData.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryMidtermData
{
    public class LibraryContext
    {
        public LibraryContext() { }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (IDbConnection db = new SqlConnection("Data Source=THISISNOISE\\SQLEXPRESS;Initial Catalog=librarydb;Integrated Security=True"))
            {
                users = db.Query<User>("Select * From dbo.Users").ToList();
            }

            return users;
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            using (IDbConnection db = new SqlConnection("Data Source=THISISNOISE\\SQLEXPRESS;Initial Catalog=librarydb;Integrated Security=True"))
            {
                books = db.Query<Book>("Select * From dbo.Book").ToList();
            }

            return books;
        }

        public void UpdateList()
        {
            List<Book> books = new List<Book>();
            using (IDbConnection db = new SqlConnection("Data Source=THISISNOISE\\SQLEXPRESS;Initial Catalog=librarydb;Integrated Security=True"))
            {
                books = db.Query<Book>("Select * From dbo.Book").ToList();
            }
        }
    }
}