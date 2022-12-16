using backend_iss.DTOs;
using backend_iss.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_iss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public BooksController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("Add"), Authorize(Roles = "Librerian")]
        public async Task<ActionResult> AddBook(addBookDto book)
        {
            Books newBook = new Books();
            newBook.BookName = book.bookName;
            newBook.BookType = book.bookType;
            _dataContext.Books.Add(newBook);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("AllBooks"),Authorize()]
        public async Task<ActionResult> GetAllBooks()
        {
            return Ok(await _dataContext.Books.ToListAsync());
        }

        [HttpPost]
        [Route("Edit"), Authorize(Roles = "Librerian")]
        public async Task<ActionResult> Edit(Books book)
        {
            var bookToEdit = _dataContext.Books.Find(book.Id);

            if (bookToEdit == null)
            {
                return NotFound("Book Not Found");
            }
            bookToEdit.BookName = book.BookName;
            bookToEdit.BookType = book.BookType;

            _dataContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}"), Authorize(Roles = "Librerian")]
        public async Task<ActionResult>Delete(int id)
        {
            var bookToDelete = _dataContext.Books.Find(id);
            if (bookToDelete == null)
            {
                return NotFound("Book Not Found");
            }
            _dataContext.Books.Remove(bookToDelete);
            _dataContext.SaveChanges();
            return Ok();
        }

    }
}
