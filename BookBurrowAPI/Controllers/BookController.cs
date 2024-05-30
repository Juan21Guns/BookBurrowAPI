using AutoMapper;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using BookBurrowAPI.Models.GoogleBooksApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.SqlServer.Query.Internal.SqlServerOpenJsonExpression;

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
            if (something.totalItems == 0)
            {
                return Ok("null");
            }
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

        [HttpGet("/getBookFromDb")]
        public IActionResult GetBookFromDb(int id)
        {
            if (id == 0)
            {
                return BadRequest("must be a valid id");
            }

            var book = _bookAction.GetBookFromDb(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpGet("/getOtherBooks")]
        public IActionResult GetOtherBooks(int start, int end)
        {
            var books = _bookAction.GetBooks(start, end);
            return books == null ? BadRequest("something went wrong with our servers") : Ok(books);
        }

        [HttpPost("/createBook")]
        public async Task<IActionResult> CreateBook(string url)
        {
            if (url == null)
            {
                return NotFound("input cannot be empty");
            }

            var b = await _bookAction.GetBooksFromUrl(url);

            if (b.volumeInfo?.description?.Length > 3000)
            {
                b.volumeInfo.description = b.volumeInfo.description.Substring(0, 2999);
            }

            if (b.volumeInfo?.industryIdentifiers[0].identifier.Length > 13)
            {
                b.volumeInfo.industryIdentifiers[0].identifier = b.volumeInfo?.industryIdentifiers[0].identifier.Substring(0, 12);
            }

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

                int created = _bookAction.CreateBook(book);

                if (created > 0)
                {
                    return Ok(created);
                } else if (created == 0)
                {
                    return BadRequest("book already exists");
                }

                return BadRequest("error creating book");
            }

            return BadRequest("seomthing went wrong");
        }
    }
}
