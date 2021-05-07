using EverGardenNew.Controllers;
using EverGardenNew.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EverGardenNew.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void HomeControllerIndexTest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(mockLogger.Object);
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlantsControllerIndexTest()
        {
            // Arrange
            var mockCont = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var controller = new PlantsController(mockCont.Object);
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PlantActivitiesControllerIndexTest()
        {
            // Arrange
            var mockCont = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var controller = new PlantActivitiesController(mockCont.Object);
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CategoryEdibleControllerIndexTest()
        {
            // Arrange
            var mockCont = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var controller = new CategoryEdiblesController(mockCont.Object);
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CategoryPlaceControllerIndexTest()
        {
            // Arrange
            var mockCont = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var controller = new CategoryPlacesController(mockCont.Object);
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}