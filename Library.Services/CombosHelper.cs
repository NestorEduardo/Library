using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Entities;

namespace Library.Services
{
    public class CombosHelper : IDisposable
    {
        private static DataContext db = new DataContext();

        public static List<BookType> GetBookTypes()
        {
            var bookType = db.BookTypes.ToList();

            bookType.Add(new BookType
            {
                BookTypeID = 0,
                Description = "[Select a booktype]"
            });

            return bookType.OrderBy(bt => bt.Description).ToList();
        }

        public static List<Writer> GetWriters()
        {
            var writer = db.Writers.ToList();

            writer.Add(new Writer
            {
                WriterID = 0,
                Name = "[Select a writer]",
                Biography = "",            
            });

            return writer.OrderBy(w => w.Name).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
