using Library.Domain.Abstract;
using Library.Domain.Entities;
using System.Collections.Generic;
using System;

namespace Library.Domain.Concrete
{
    public class EFBookTypeRepository : IBookTypeRepository
    {
        private DataContext context = new DataContext();

        public IEnumerable<BookType> BookTypes
        {
            get { return context.Products; }
        }
    }
}
