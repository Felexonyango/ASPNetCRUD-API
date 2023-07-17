using BookApp.Context;
using BookApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BookApp.Errors;
namespace BookApp.Controllers
{
    [Authorize]
     [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        public BookController(ApplicationDbContext context)
        {
            _dbcontext = context;

        }
        [HttpGet("allbooks")]
        public async Task<ActionResult<IEnumerable<Book>>>GetBooks(){

            if (_dbcontext.Books == null)  return NotFound(new ApiResponse(404));

               var books =  await _dbcontext.Books.ToListAsync();
                 var responseBody = new
            {
                message = "Successfully retrieved all books",
                result = books
            };
         
            return Ok(responseBody);
            

}
        [HttpGet("getbook/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {

            if (_dbcontext.Books == null)  return NotFound(new ApiResponse(404));
           
            var book = await _dbcontext.Books.FindAsync(id);
            if(book == null) return NotFound(new ApiResponse(404));
           
               var responseBody = new
            {
                message = "Successfully retrieved product",
                result = book
            };
         
            return Ok(responseBody);
          

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
            if(id != book.Id) return BadRequest(new ApiResponse(400));
          
            _dbcontext.Entry(book).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!checkBookAvailable(id)) return NotFound(new ApiResponse(400));
               
               
            }
            return Ok();
        }
        private bool checkBookAvailable(int id)
        {
            return _dbcontext.Books.Any(book => book.Id == id);
        }

        [HttpDelete("deletebook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (_dbcontext.Books == null) return NotFound(new ApiResponse(404));
            
            var book = await  _dbcontext.Books.FindAsync(id);
            if (book != null) return NotFound(new ApiResponse(400));
           
            _dbcontext.Books.Remove(book);
            await _dbcontext.SaveChangesAsync();

            return Ok();

            // return okay
        }




    }
}
