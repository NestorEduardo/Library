namespace Library.Domain.Entities
{
    public class Book
    {
        public int BookID { get; set; }

        public string Title { get; set; }

        public virtual BookType BookType { get; set; }

        public int BookTypeID { get; set; }

        public virtual Writer Writer { get; set; }

        public int WriterID { get; set; }

        public string Plot { get; set; }

        public string ISBN { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}
