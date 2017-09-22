using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Web.Controllers;
using Library.Web.HtmlHelpers;
using Library.Web.Models;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Library.UnitTests
{
    public class BookTypeTests
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
                new BookType {BookTypeID = 11, Description = "Poetry"}
            });

            BookTypesController controller = new BookTypesController(mock.Object);
            controller.PageSize = 3;

            // Act
            BookTypesListViewModel result = (BookTypesListViewModel)controller.List(2).Model;

            // Assert
            BookType[] bookTypeArray = result.BookTypes.ToArray();
            Assert.True(bookTypeArray.Length == 3);
            Assert.Equal(bookTypeArray[0].Description, "Romance");
            Assert.Equal(bookTypeArray[1].Description, "Mystery");
            Assert.Equal(bookTypeArray[2].Description, "Horror");
        }

        [Fact]
        public void CanGeneratePageLinks()
        {
            HtmlHelper myHelper = null;

            // Arrange
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.Equal(@"<a class=""btn btn-default"" href=""Page1"">1</a>" +
                @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
                @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [Fact]
        public void CanSendPaginationViewModel()
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
            BookTypesController controller = new BookTypesController(mock.Object);
            controller.PageSize = 3;

            // Act
            BookTypesListViewModel result = (BookTypesListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(pageInfo.CurrentPage, 2);
            Assert.Equal(pageInfo.ItemsPerPage, 3);
            Assert.Equal(pageInfo.TotalItems, 11);
            Assert.Equal(pageInfo.TotalPages, 4);
        }

        [Fact]
        public void CanEditBookType()
        {
            // Arrange
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            mock.Setup(m => m.BookTypes).Returns(new BookType[] {
                new BookType {BookTypeID = 1, Description = "Science fiction"},
                new BookType {BookTypeID = 2, Description = "Satire"},
                new BookType {BookTypeID = 3, Description = "Drama"},
                new BookType {BookTypeID = 4, Description = "Romance"},
            });

            // Arrange
            BookTypesController target = new BookTypesController(mock.Object);

            // Act
            BookType bookType1 = target.Edit(1).ViewData.Model as BookType;
            BookType bookType2 = target.Edit(2).ViewData.Model as BookType;
            BookType bookType3 = target.Edit(3).ViewData.Model as BookType;

            // Assert
            Assert.Equal(1, bookType1.BookTypeID);
            Assert.Equal("Satire", bookType2.Description);
        }

        [Fact]
        public void CannotEditNonexistentBookType()
        {
            // Arrange
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            mock.Setup(m => m.BookTypes).Returns(new BookType[] {
                new BookType {BookTypeID = 1, Description = "Science fiction"},
                new BookType {BookTypeID = 2, Description = "Satire"},
                new BookType {BookTypeID = 3, Description = "Drama"},
                new BookType {BookTypeID = 4, Description = "Romance"},
            });

            // Arrange
            BookTypesController target = new BookTypesController(mock.Object);

            // Act
            BookType bookType1 = target.Edit(5).ViewData.Model as BookType;

            // Assert
            Assert.Null(bookType1);
        }

        [Fact]
        public void CanSaveValidChanges()
        {
            // Arrange
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            BookTypesController target = new BookTypesController(mock.Object);
            BookType bookType = new BookType { Description = "Romance" };

            // Act - try to save the booktype.
            ActionResult result = target.Edit(bookType);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveBookType(bookType));

            // Assert - check the method result type
            Assert.IsNotType(typeof(ViewResult), result);
        }

        [Fact]
        public void CannotSaveInvalidChanges()
        {
            // Arrange - create mock repository
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();

            // Arrange - create the controller
            BookTypesController target = new BookTypesController(mock.Object);

            // Arrange - create a booktype
            BookType bookType = new BookType { Description = "Test" };

            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            ActionResult result = target.Edit(bookType);

            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveBookType(It.IsAny<BookType>()), Times.Never());

            // Assert - check the method result type
            Assert.IsType(typeof(ViewResult), result);
        }

        [Fact]
        public void CanDeleteValidValidBookTypes()
        {
            // Arrange - create a BookType
            BookType bookType = new BookType { BookTypeID = 2, Description = "Test" };

            // Arrange - create the mock repository
            Mock<IBookTypeRepository> mock = new Mock<IBookTypeRepository>();
            mock.Setup(m => m.BookTypes).Returns(new BookType[]
            {
                new BookType {BookTypeID = 1, Description = "P1"}, bookType,
                new BookType {BookTypeID = 3, Description = "P3"},
            });

            // Arrange - create the controller
            BookTypesController target = new BookTypesController(mock.Object);

            // Act - delete the booktype
            target.Delete(bookType.BookTypeID);

            // Assert - ensure that the repository delete method was
            // called with the correct BookType
            mock.Verify(m => m.DeleteBookType(bookType.BookTypeID)); 
        }
    }
}