using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Writer
    {
        [Key]
        public int WriterID { get; set; }

        [Required(ErrorMessage = "Please enter a {0}.")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a {0}.")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Biography { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
