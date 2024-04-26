using BookBurrowAPI.Data;
using BookBurrowAPI.Interfaces;
using BookBurrowAPI.Models;
using Newtonsoft.Json;

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
        public async Task<List<GBApiItemsModel>> GetBooksFromApi()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://www.googleapis.com/books/v1/volumes?q=jujutsu+kaisen+isbn=%229781974733767%22+inauthor=Gege"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.Content);
                    GoogleBooksApiModel? results = await response.Content.ReadFromJsonAsync<GoogleBooksApiModel>();
                    return results.items;
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
