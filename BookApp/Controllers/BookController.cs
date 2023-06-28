using BookApp.Context;
using BookApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        public BookController(ApplicationDbContext context)
        {
            _dbcontext = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>>GetBooks(){

            if (_dbcontext.Books == null) {
                return NotFound();
            }
            return await _dbcontext.Books.ToListAsync();

}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {

            if (_dbcontext.Books == null)
            {
                return NotFound();
            }
            var book = await _dbcontext.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            return book;

        }
        [HttpPost]
        public async  Task<ActionResult<Book>>CreateBook(Book book)
        {

          _dbcontext.Books.Add(book);
            await _dbcontext.SaveChangesAsync();

            return Ok();

        }



    }
}
