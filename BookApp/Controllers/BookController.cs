using BookApp.Context;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookApp.Errors;
using AutoMapper;
using BookApp.DTos;
using BookApp.Services;

namespace BookApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
             public UserService _userService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbcontext;
        public BookController(
            ApplicationDbContext context, 
        IMapper mapper,
        UserService userService
        
        )
        {
            _dbcontext = context;
            _mapper = mapper;
            _userService = userService;

        }
        [HttpGet("allbooks")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            if (_dbcontext.Books == null)
                return NotFound(new ApiResponse(404));

            var books = await _dbcontext.Books.ToListAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);

            var responseBody = new
            {
                message = "Successfully retrieved all books",
                result = bookDtos
            };

            return Ok(responseBody);
        }

        [HttpGet("getbook/{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {

            if (_dbcontext.Books == null) return NotFound(new ApiResponse(404));

            var book = await _dbcontext.Books.FindAsync(id);
            var bookDtos = _mapper.Map<BookDto>(book);
            if (book == null) return NotFound(new ApiResponse(404));

            var responseBody = new
            {
                message = "Successfully retrieved book",
                result = bookDtos
            };

            return Ok(responseBody);


        }
        [HttpPost("create")]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            var currentUser = await _userService.GetCurrentUser();
            bookDto.UserId = currentUser.Id;
             var book = _mapper.Map<Book>(bookDto); // Map BookDto to Book entity
            _dbcontext.Books.Add(book);
            await _dbcontext.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id) return BadRequest(new ApiResponse(400));

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

            var book = await _dbcontext.Books.FindAsync(id);
            if (book != null) return NotFound(new ApiResponse(400));

            _dbcontext.Books.Remove(book);
            await _dbcontext.SaveChangesAsync();

            return Ok();

            // return okay
        }




    }
}
