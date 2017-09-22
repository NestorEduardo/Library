using Library.Domain.Abstract;
using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Domain.Concrete
{
    public class EFWriterRepository : IWriterRepository
    {
        private DataContext context = new DataContext();

        public IEnumerable<Writer> Writers
        {
            get { return context.Writers; }
        }

        public void SaveWriter(Writer writer)
        {
            if (writer.WriterID == 0)
            {
                context.Writers.Add(writer);
            }
            else
            {
                Writer dbEntry = context.Writers.Find(writer.WriterID);

                if (dbEntry != null)
                {
                    dbEntry.Name = writer.Name;
                    dbEntry.Biography = writer.Biography;
                    dbEntry.ImageData = writer.ImageData;
                    dbEntry.ImageMimeType = writer.ImageMimeType;
                }
            }

            context.SaveChanges();
        }

        public Writer DeleteWriter(int writerID)
        {
            Writer dbEntry = context.Writers.Find(writerID);

            if (dbEntry != null)
            {
                context.Writers.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
