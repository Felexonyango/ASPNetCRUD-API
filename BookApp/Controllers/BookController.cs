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
        [HttpPost("create")]
        public async  Task<ActionResult<Book>>CreateBook(Book book)
        {

          _dbcontext.Books.Add(book);
            await _dbcontext.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if(id != book.Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(book).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!checkBookAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool checkBookAvailable(int id)
        {
            return _dbcontext.Books.Any(book => book.Id == id);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (_dbcontext.Books == null) {
                return NotFound();
            }
                

            var book = await  _dbcontext.Books.FindAsync(id);
            Console.WriteLine(book);
            Console.ReadKey();

            if (book != null)
            {
                return NotFound();

            }
            _dbcontext.Books.Remove(book);
            await _dbcontext.SaveChangesAsync();

            return Ok();
        }


    }
}
