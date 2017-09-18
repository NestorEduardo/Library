using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Web.Models
{
    public class BookTypesListViewModel
    {
        public IEnumerable<BookType> BookTypes { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}