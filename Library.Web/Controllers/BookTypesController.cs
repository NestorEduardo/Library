using Library.Domain.Abstract;
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
            this.repository = bookTypeRepository;
        }

        public ViewResult List()
        {
            return View(repository.BookTypes);
        }

        public ViewResult List(int page = 1)
        {
            return View(repository.BookTypes.OrderBy(bt => bt.BookTypeID).Skip((page - 1) * PageSize).Take(PageSize));
        }
    }
}