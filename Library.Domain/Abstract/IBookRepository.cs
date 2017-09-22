﻿using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Domain.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
    }
}
