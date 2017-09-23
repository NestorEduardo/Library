using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Services;
using Library.Web.Models;
using System.Linq;
using System.Web;
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

        public FileContentResult GetImage(int bookId)
        {
            Book book = repository.Books.FirstOrDefault(b => b.BookID == bookId);

            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }

            return null;
        }

        public ViewResult Edit(int bookId)
        {
            Book book = repository.Books.FirstOrDefault(b => b.BookID == bookId);
            ViewBag.BookTypeID = new SelectList(CombosHelper.GetBookTypes(), "BookTypeID", "Description", book.BookType);
            ViewBag.WriterID = new SelectList(CombosHelper.GetWriters(), "WriterID", "Name", book.WriterID);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }

                repository.SaveBook(book);
                TempData["message"] = string.Format($"{book.Title} has been saved.");
                return RedirectToAction("List");
            }

            // there is something wrong with the data values
            ViewBag.BookTypeID = new SelectList(CombosHelper.GetBookTypes(), "BookTypeID", "Description", book.BookType);
            ViewBag.WriterID = new SelectList(CombosHelper.GetWriters(), "WriterID", "Name", book.WriterID);
            return View(book);
        }

        public ViewResult Create()
        {
            ViewBag.BookTypeID = new SelectList(CombosHelper.GetBookTypes(), "BookTypeID", "Description");
            ViewBag.WriterID = new SelectList(CombosHelper.GetWriters(), "WriterID", "Name");
            return View("Edit", new Book());
        }

        [HttpPost]
        public ActionResult Delete(int bookId)
        {
            Book deletedBook = repository.DeleteBook(bookId);

            if (deletedBook != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedBook.Title);
            }

            return RedirectToAction("List");
        }
    }
}