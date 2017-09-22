using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Writer
    {
        public int WriterID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
