using BookBurrowAPI.Models;
using BookBurrowAPI.Models.GoogleBooksApi;

namespace BookBurrowAPI.Interfaces
{
    public interface IBookRepository
    {
        /*        https://www.googleapis.com/books/v1/volumes?q=jujutsu+kaisen+isbn=%229781974733767%22+inauthor=Gege&callback=handleResponse
        */

        bool SaveChanges();
        Task<GBAModels> GetBooksFromApi(string? title, string? isbn, string? author);
        Task<GBAModel2> GetBooksFromUrl(string url);
        Books? GetBookFromDb(int id);
        bool BookExist(Books book);
        int CreateBook(Books book);
        ICollection<Books>? GetBooks(int start, int end = 10);
    }
}
