using System.Data.Entity;

namespace Library.Domain.Entities
{
    class DataContext : DbContext
    {
        public DbSet<BookType> BookTypes { get; set; }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
