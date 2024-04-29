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
        bool BookExist(Books book);
        bool CreateBook(Books book);

        //Get book by (title, isbn) q=name+separated+by+commas+isbn="string"+inauthor="author"
        //%20 is whitespace
        //auto mapper to filter results
        //https://www.googleapis.com/books/v1/volumes? 

        //Get specific book (items.selfLink)
        //used to get image urls and after clicking on search thumbnail

        //Add book to group (add to db)
        //Update private group with book
        //Check db if it is already added

        //Get book by Id (int id)
        //for groups 

        //Get books from db 
        //what others are reading

        //Create book from scratch
        //can't find it from GoogleBooks

        //delete book 
        //only if error 
        //Update books in db
        //only if there is an error 
    }
}
