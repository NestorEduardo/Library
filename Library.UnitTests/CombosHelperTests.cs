using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Services;
using Library.Web.Controllers;
using Library.Web.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.UnitTests
{
    public class CombosHelperTests
    {
        [Fact]
        public void CombosHelperGetCorrectResults()
        {
            // Arrange
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            mock.Setup(m => m.BookTypes).Returns(new BookType[] {
                new BookType {BookTypeID = 1, Description = "Science fiction"},
                new BookType {BookTypeID = 2, Description = "Satire"},
            });

            BookTypesController controller = new BookTypesController(mock.Object);
            controller.PageSize = 3;

            // Act
            BookTypesListViewModel result = (BookTypesListViewModel)controller.List(2).Model;
            List<BookType> bookTypes = CombosHelper.GetBookTypes();
            var firstElement = bookTypes.Where(bt => bt.BookTypeID == 0).First();

            // Assert
            BookType[] bookTypeArray = result.BookTypes.ToArray();
            Assert.Equal(firstElement.Description, "[Select a booktype]");
            Assert.Equal(bookTypeArray.Length, bookTypes.Count - 1);
        }
    }
}
