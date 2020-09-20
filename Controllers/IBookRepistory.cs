using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookAPI.Controllers
{
    public interface IBookRepistory
    {
        void Delete(int id);
        IEnumerable<Book> Get();
        Book GetById(int id);
        Book Insert(Book book);
        Book Update(int id, [FromBody] Book book);
    }
}