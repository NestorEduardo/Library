using Library.Domain.Abstract;
using System.Collections.Generic;
using Library.Domain.Entities;

namespace Library.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private DataContext context = new DataContext();

        public IEnumerable<Book> Books
        {
            get { return context.Books; }
        }

        public void SaveBook(Book book)
        {
            if (book.BookID == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookID);

                if (dbEntry != null)
                {
                    dbEntry.Title = book.Title;
                    dbEntry.Plot = book.Plot;
                    dbEntry.ISBN = book.ISBN;
                    dbEntry.WriterID = book.WriterID;
                    dbEntry.BookTypeID = book.BookTypeID;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                }
            }

            context.SaveChanges();
        }

        public Book DeleteBook(int bookID)
        {
            Book dbEntry = context.Books.Find(bookID);

            if (dbEntry != null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
