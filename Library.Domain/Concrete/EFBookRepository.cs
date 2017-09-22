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
    }
}
