using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    public class BookRepistory : IBookRepistory
    {
        private IDataAccess _dataAccess = new DataAccess();

        public IEnumerable<Book> Get()
        {
            string query = "select * from Book";

            var list = _dataAccess.Query<Book>(query, CommandType.Text, null);
            return list;
        }


        public Book GetById(int id)
        {
            string query = "select * from Book where Id = " + id;

            var list = _dataAccess.Query<Book>(query, CommandType.Text, null).SingleOrDefault();
            return list;
        }

        public Book Insert(Book book)
        {
            var myBook = new Book();

            string query = "INSERT INTO Book ( Title,Rating) VALUES ( @title, @rating); SELECT SCOPE_IDENTITY()";

            SqlParameter[] parameters =
            {
                new SqlParameter("@title", book.Title),
                new SqlParameter("@rating", book.Rating)
            };

            int id = Convert.ToInt32(_dataAccess.ExecuteScalar(query, CommandType.Text, parameters));

            myBook = book;

            return myBook;

        }

        public Book Update(int id, [FromBody]Book book)
        {
            var myBook = new Book();
            string query = "update Book set title = @title, rating = @rating where id = @id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@title", book.Title),
                new SqlParameter("@rating", book.Rating),
                new SqlParameter("@id", id)
            };

            _dataAccess.ExecuteNonQuery(query, CommandType.Text, parameters);

            myBook = new Book() { ID = id, Rating = book.Rating, Title = book.Title };

            return myBook;
        }

        public void Delete(int id)
        {
            string query = "delete from Book where id = @id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id)
            };

            _dataAccess.ExecuteNonQuery(query, CommandType.Text, parameters);

        }

    }
}
