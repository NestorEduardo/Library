using Library.Domain.Abstract;
using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Domain.Concrete
{
    public class EFBookTypeRepository : IBookTypeRepository
    {
        private DataContext context = new DataContext();

        public IEnumerable<BookType> BookTypes
        {
            get { return context.BookTypes; }
        }

        public void SaveBookType(BookType bookType)
        {
            if (bookType.BookTypeID == 0)
            {
                context.BookTypes.Add(bookType);
            }
            else
            {
                BookType dbEntry = context.BookTypes.Find(bookType.BookTypeID);

                if (dbEntry != null)
                {
                    dbEntry.Description = bookType.Description;
                }
            }

            context.SaveChanges();
        }

        public BookType DeleteBookType(int bookTypeID)
        {
            BookType dbEntry = context.BookTypes.Find(bookTypeID);

            if (dbEntry != null)
            {
                context.BookTypes.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
