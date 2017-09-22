using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class BookType
    {
        public int BookTypeID { get; set; }

        [Required(ErrorMessage = "Please enter a {0}.")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
