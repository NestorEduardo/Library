using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Web.Controllers;
using Moq;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Library.UnitTests
{
    public class ImageTests
    {
        [Fact]
        public void CanRetrieveImageData()
        {
            // Arrange - create a Writer with image data.
            Writer writer = new Writer
            {
                WriterID = 2,
                Name = "Juan Bosch",
                Biography = "Juan Bosch Juan Bosch Juan Bosch",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Arrange - create the mock repository
            Mock<IWriterRepository> mock = new Mock<IWriterRepository>();
            mock.Setup(m => m.Writers).Returns(new Writer[]
            {new Writer {WriterID = 1, Name = "P1", Biography = "B1"}, writer,
                new Writer {WriterID = 3, Name = "P3", Biography = "B2"}
            }.AsQueryable());

            // Arrange - create the controller
            WritersController target = new WritersController(mock.Object);

            // Act - call the GetImage action method
            ActionResult result = target.GetImage(2);

            // Assert
            Assert.NotNull(result);
            Assert.IsType(typeof(FileContentResult), result);
            Assert.Equal(writer.ImageMimeType, ((FileResult)result).ContentType);
        }

        [Fact]
        public void CannotRetrieveImageDataForInvalidID()
        {
            // Arrange - create the mock repository
            Mock<IWriterRepository> mock = new Mock<IWriterRepository>();
            mock.Setup(m => m.Writers).Returns(new Writer[] {
                new Writer {WriterID = 1, Name = "P1", Biography = "B1"},
                new Writer {WriterID = 2, Name = "P2", Biography = "B2"}
            }.AsQueryable());

            // Arrange - create the controller
            WritersController target = new WritersController(mock.Object);

            // Act - call the GetImage action method
            ActionResult result = target.GetImage(100);

            // Assert
            Assert.Null(result);
        }
    }
}
