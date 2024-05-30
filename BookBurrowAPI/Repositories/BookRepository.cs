using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using BookBurrowAPI.Models.GoogleBooksApi;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BookBurrowAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        // https://www.googleapis.com/books/v1/volumes?q=jujutsu+kaisen+isbn=%229781974733767%22+inauthor=Gege&callback=handleResponse

        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            try
            {
                int didSave = _context.SaveChanges();
                return didSave > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<GBAModels> GetBooksFromApi(string? title, string? isbn, string? author)
        {
            //string name separated by +, +, isbn=string, +, inauthor=string 
            string qTitle = "";
            string qIsbn = "";
            string qAuthor = "";

            string[] combine = Array.Empty<string>();

            if (!title.IsNullOrEmpty())
            {
                /*                string[] splitTitle = title.Split(" ");
                                string finalTitle = string.Join("+intitle:", splitTitle);
                                ;*/

                qTitle = $"intitle:{title}";
                combine = combine.Append(qTitle).ToArray();
            }

            if (!isbn.IsNullOrEmpty())
            {
                qIsbn = $"isbn:{isbn}";
                combine = combine.Append(qIsbn).ToArray();
            }

            if (!author.IsNullOrEmpty())
            {
                string[] splitAuthor = author.Split(" ");
                string finalAuthor = string.Join("+inauthor:", splitAuthor);
                qAuthor = $"inauthor:{finalAuthor}";
                combine = combine.Append(qAuthor).ToArray();
            }

            string combined = string.Join("+", combine);

            string testUrl = $"https://www.googleapis.com/books/v1/volumes?q={combined}";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(testUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    GBAModels? results = await response.Content.ReadFromJsonAsync<GBAModels>();

                    return results == null ? throw new Exception("results is null") : results;
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<GBAModel2> GetBooksFromUrl(string url)
        {
            if (url.IsNullOrEmpty())
            {
                throw new Exception("url is null");
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    GBAModel2? results = await response.Content.ReadFromJsonAsync<GBAModel2>();

                    return results == null ? throw new Exception("results is null") : results;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public Books? GetBookFromDb(int id)
        {
            try
            {
                return _context.Books.Where(c => c.BookId == id).First();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool BookExist(Books book)
        {
            return _context.Books.Where(c => (c.BookTitle == book.BookTitle) && (c.BookAuthor == book.BookAuthor) && (c.PageCount == book.PageCount)).Any();
        }

        public int CreateBook(Books book)
        {
            if (book != null && !BookExist(book))
            {
                _context.Add(book);
                return SaveChanges() == true ? book.BookId : -1;
            }
            return 0;
        }

        public ICollection<Books>? GetBooks(int start, int end)
        {
            try
            {
                var collection = _context.Books
                .OrderByDescending(c => c.BookId)
                .Skip(start)
                .Take(end - start)
                .ToList();
                return collection;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
