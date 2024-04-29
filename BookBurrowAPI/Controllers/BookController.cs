using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using BookBurrowAPI.Models.GoogleBooksApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public async Task<IActionResult> GetBooks(string? title, string? isbn, string? author)
        {
            if (title  == null && isbn == null && author == null)
            {
                return BadRequest("Please enter at least one field");
            }

            var something = await _bookAction.GetBooksFromApi(title, isbn, author);
            return Ok(something);
        }

        [HttpGet("/booksApiCloser")]
        public async Task<IActionResult> GetBook(string url)
        {
            if (url == null)
            {
                return BadRequest("input cannot be empty");
            }

            var something = await _bookAction.GetBooksFromUrl(url);
            return Ok(something);
        }

        [HttpPost("/createBook")]
        public async Task<IActionResult> CreateBook(string url)
        {
            if (url == null)
            {
                return NotFound("input cannot be empty");
            }

            var b = await _bookAction.GetBooksFromUrl(url);
            
            if (b != null)
            {
                Books book = new Books()
                {
                    BookTitle = b.volumeInfo.title,
                    BookSubtitle = b.volumeInfo.subtitle,
                    BookAuthor = b.volumeInfo.authors[0],
                    BookDescription = b.volumeInfo.description,
                    PageCount = b.volumeInfo.pageCount,
                    PreviewLink = b.volumeInfo.previewLink,
                    BookImage = b.volumeInfo.imageLinks.large,
                    BookSmallImage = b.volumeInfo.imageLinks.thumbnail,
                    BookISBN = b.volumeInfo.industryIdentifiers[0].identifier
                };

                bool created = _bookAction.CreateBook(book);
                return created ? Ok(book) : BadRequest("something went wrong");
            }

            return BadRequest("seomthing went wrong");
        }
    }
}
