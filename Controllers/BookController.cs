using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase

    { 
        string connectionString = @"Data Source=(local);Initial Catalog=Demo;Integrated Security=True;";
        private readonly ILogger<BookController> _logger;
        private IBookRepistory _bookRepistory = null;

        public BookController(IBookRepistory bookRepistory, ILogger<BookController> logger)
        {
            _logger = logger;
            _bookRepistory = bookRepistory;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _bookRepistory.Get();
        }


        [HttpGet]
        public Book GetById(int id)
        {
            Book book =   _bookRepistory.GetById(id);
            book.Title = "Mr" + book.Title;
            return book;
        }

        [HttpPost]
        public Book Insert(Book book)
        {
            return _bookRepistory.Insert(book);

        }


        [HttpPut("update/{id}")]
        public Book Update(int id, [FromBody]Book book)
        {
            return _bookRepistory.Update(id, book);
        }

        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
            _bookRepistory.Delete(id);

        }
    }
}
