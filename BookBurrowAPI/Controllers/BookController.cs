using AutoMapper;
using BookBurrowAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookBurrowAPI.Controllers
{
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookAction;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookAction, IMapper mapper)
        {
            _bookAction = bookAction;
            _mapper = mapper;
        }

        [HttpGet("/booksApi")]
        public async Task<IActionResult> GetBooks()
        {
            var something = await _bookAction.GetBooksFromApi();

            return Ok(something);
        }
    }
}
