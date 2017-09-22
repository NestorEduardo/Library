using Library.Domain.Abstract;
using Library.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 4;

        public BooksController(IBookRepository bookRepository)
        {
            repository = bookRepository;
        }

        public ViewResult List(int page = 1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.Books.OrderBy(b => b.BookID).Skip((page - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Books.Count()
                }
            };

            return View(model);
        }
    }
}