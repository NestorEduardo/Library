using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Please enter a {0}.")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Book")]
        [Range(1, double.MaxValue, ErrorMessage = "Please select a {0}.")]
        public int BookTypeID { get; set; }

        [Display(Name = "Writer")]
        [Range(1, double.MaxValue, ErrorMessage = "Please select a {0}.")]
        public int WriterID { get; set; }

        [Required(ErrorMessage = "Please enter a {0}.")]
        [StringLength(500, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Plot { get; set; }

        [Required(ErrorMessage = "Please enter a {0}.")]
        [StringLength(13, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 10)]
        public string ISBN { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        public virtual BookType BookType { get; set; }

        public virtual Writer Writer { get; set; }
    }
}
