using Dapper;
using LibraryMidtermData.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LibraryMidtermData
{
    public class LibraryContext
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json", optional: true)
                .Build();

        public LibraryContext() 
        {
            
        }        

        public List<User> GetUsers()
        {          
            List<User> users = new List<User>();
            using (IDbConnection db = new SqlConnection(configuration["Settings: connectionString"]))
            {
                users = db.Query<User>("Select * From dbo.Users").ToList();
            }

            return users;
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            using (IDbConnection db = new SqlConnection(configuration["Settings: connectionString"]))
            {
                books = db.Query<Book>("Select * From dbo.Book").ToList();
            }

            return books;
        }

        public void UpdateList()
        {
            List<Book> books = new List<Book>();
            using (IDbConnection db = new SqlConnection(configuration["Settings: connectionString"]))
            {
                books = db.Query<Book>("Select * From dbo.Book").ToList();
            }
        }
    }
}