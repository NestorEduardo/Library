using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Web.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void CanPaginate()
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
                new BookType {BookTypeID = 11, Description = "Poetry"},

            });

            BookTypesController controller = new BookTypesController(mock.Object);
            controller.PageSize = 3;

            // Act
            IEnumerable<BookType> result = (IEnumerable<BookType>)controller.List(3).Model;

            // Assert
            BookType[] bookTypeArray = result.ToArray();
            Assert.True(bookTypeArray.Length == 3);
            Assert.Equal(bookTypeArray[0].Description, "Self help");
            Assert.Equal(bookTypeArray[1].Description, "Health");
            Assert.Equal(bookTypeArray[2].Description, "Guide");
        }
    }
}
