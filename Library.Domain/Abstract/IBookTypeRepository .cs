using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Domain.Abstract
{
    public interface IBookTypeRepository
    {
        IEnumerable<BookType> BookTypes { get; }
    }
}
