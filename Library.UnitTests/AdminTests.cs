using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Web.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.UnitTests
{
    public class AdminTests
    {
        [Fact]
        public void Page_Contains_All_BookTypes()
        {
            // Arrange
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            mock.Setup(m => m.BookTypes).Returns(new BookType[] {
                new BookType {BookTypeID = 1, Description = "Science fiction"},
                new BookType {BookTypeID = 2, Description = "Satire"},
                new BookType {BookTypeID = 3, Description = "Drama"},
                new BookType {BookTypeID = 4, Description = "Romance"},
                new BookType {BookTypeID = 5, Description = "Mystery"},
                new BookType {BookTypeID = 6, Description = "Horror"},
                new BookType {BookTypeID = 7, Description = "Self help"},
                new BookType {BookTypeID = 8, Description = "Health"},
                new BookType {BookTypeID = 9, Description = "Guide"},
                new BookType {BookTypeID = 10, Description = "Travel"},
                new BookType {BookTypeID = 11, Description = "Poetry"}
            });

            // Arrange
            HomeController target = new HomeController(mock.Object);

            // Action
            BookType[] result = ((IEnumerable<BookType>)target.BookTypes().ViewData.Model).ToArray();

            // Assert
            Assert.Equal(result.Length, 11);
            Assert.Equal("Science fiction", result[0].Description);
            Assert.Equal("Satire", result[1].Description);
            Assert.Equal("Drama", result[2].Description);
        }
    }
}
