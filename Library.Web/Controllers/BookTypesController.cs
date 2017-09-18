using Library.Domain.Abstract;
using Library.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class BookTypesController : Controller
    {
        private IBookTypeRepository repository;
        public int PageSize = 4;


        public BookTypesController(IBookTypeRepository bookTypeRepository)
        {
            repository = bookTypeRepository;
        }

        public ViewResult List(int page = 1)
        {
            BookTypesListViewModel model = new BookTypesListViewModel
            {
                BookTypes = repository.BookTypes.OrderBy(p => p.BookTypeID).Skip((page - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.BookTypes.Count()
                }
            };

            return View(model);

        }
    }
}