using Library.Domain.Abstract;
using Library.Domain.Entities;
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
                BookTypes = repository.BookTypes.OrderBy(bt => bt.BookTypeID).Skip((page - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.BookTypes.Count()
                }
            };

            return View(model);
        }

        public ViewResult Edit(int bookTypeId)
        {
            BookType bookType = repository.BookTypes.FirstOrDefault(bt => bt.BookTypeID == bookTypeId);
            return View(bookType);
        }

        [HttpPost]
        public ActionResult Edit(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBookType(bookType);
                TempData["message"] = string.Format($"{bookType.Description} has been saved.");
                return RedirectToAction("List");
            }

            // there is something wrong with the data values
            return View(bookType);
        }

        public ViewResult Create()
        {
            return View("Edit", new BookType());
        }

        [HttpPost]
        public ActionResult Delete(int bookTypeId)
        {
            BookType deletedBookType = repository.DeleteBookType(bookTypeId);

            if (deletedBookType != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedBookType.Description);
            }

            return RedirectToAction("List");
        }
    }
}