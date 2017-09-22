using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Web.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}