namespace FoodDeliveryAPI.Dtos.Tests
{
    [TestFixture]
    public class CoordinateDtoTests
    {
        [Test]
        public void Constructor_ValidCoordinateString_InitializesProperties()
        {
            // Arrange
            string validCoordinateString = "12.345,67.890";

            // Act
            var coordinateDto = new CoordinateDto(validCoordinateString);

            // Assert
            Assert.That(coordinateDto.Latitude, Is.EqualTo(12.345).Within(0.001));
            Assert.That(coordinateDto.Longitude, Is.EqualTo(67.890).Within(0.001));
        }

        [Test]
        public void Constructor_InvalidCoordinateString_ThrowsArgumentException()
        {
            // Arrange
            string invalidCoordinateString = "12.345";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new CoordinateDto(invalidCoordinateString));
        }

        [Test]
        public void Constructor_NonNumericCoordinateString_ThrowsArgumentException()
        {
            // Arrange
            string nonNumericCoordinateString = "12.345,invalid";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new CoordinateDto(nonNumericCoordinateString));
        }

        [Test]
        public void Constructor_EmptyCoordinateString_ThrowsArgumentException()
        {
            // Arrange
            string emptyCoordinateString = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new CoordinateDto(emptyCoordinateString));
        }
    }
}