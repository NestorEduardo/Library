using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Web.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class WritersController : Controller
    {
        private IWriterRepository repository;
        public int PageSize = 4;

        public WritersController(IWriterRepository writerRepository)
        {
            repository = writerRepository;
        }

        public ViewResult List(int page = 1)
        {
            WriterListViewModel model = new WriterListViewModel
            {
                Writers = repository.Writers.OrderBy(w => w.WriterID).Skip((page - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Writers.Count()
                }
            };

            return View(model);
        }

        public ViewResult Edit(int writerId)
        {
            Writer writer = repository.Writers.FirstOrDefault(bt => bt.WriterID == writerId);
            return View(writer);
        }

        [HttpPost]
        public ActionResult Edit(Writer writer, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    writer.ImageMimeType = image.ContentType;
                    writer.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(writer.ImageData, 0, image.ContentLength);
                }

                repository.SaveWriter(writer);
                TempData["message"] = string.Format($"{writer.Name} has been saved.");
                return RedirectToAction("List");
            }

            // there is something wrong with the data values
            return View(writer);
        }

        public FileContentResult GetImage(int writerId)
        {
            Writer writer = repository.Writers.FirstOrDefault(w => w.WriterID == writerId);

            if (writer != null)
            {
                return File(writer.ImageData, writer.ImageMimeType);
            }

            return null;
        }

        [HttpPost]
        public ActionResult Delete(int writerId)
        {
            Writer deletedWriter = repository.DeleteWriter(writerId);

            if (deletedWriter != null)
            {
                TempData["message"] = string.Format($"{deletedWriter.Name} was deleted.");
            }

            return RedirectToAction("List");
        }

        public ViewResult Create()
        {
            return View("Edit", new Writer());
        }
    }
}