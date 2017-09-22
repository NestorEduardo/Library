using Library.Domain.Entities;
using System.Collections.Generic;

namespace Library.Domain.Abstract
{
    public interface IWriterRepository
    {
        IEnumerable<Writer> Writers { get; }

        void SaveWriter(Writer writer);

        Writer DeleteWriter(int writerID);
    }
}
