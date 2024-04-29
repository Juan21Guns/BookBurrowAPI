using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using BookBurrowAPI.Models.GoogleBooksApi;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

            if (!title.IsNullOrEmpty())
            {
                qTitle = title;
            }

            if (!isbn.IsNullOrEmpty())
            {
                qIsbn = $"+isbn={isbn}";
            }

            if (!author.IsNullOrEmpty())
            {
                qAuthor = $"+inauthor={author}";
            }

            string testUrl = $"https://www.googleapis.com/books/v1/volumes?q={qTitle}{qIsbn}{qAuthor}";
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

        public bool BookExist(Books book)
        {
            return _context.Books.Any(c => (c.BookTitle == book.BookTitle) && (c.BookAuthor == book.BookAuthor) && (c.PageCount == book.PageCount));
        }

        public bool CreateBook(Books book)
        {
            if (book != null && !BookExist(book))
            {
                _context.Add(book);
                return SaveChanges();
            }
            return false;
        }
    }
}
