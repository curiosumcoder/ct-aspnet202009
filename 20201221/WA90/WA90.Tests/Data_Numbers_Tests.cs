using Xunit;

namespace WA90.Tests
{
    public class Data_Numbers_Tests
    {
        [Fact(DisplayName = "Numbers.GetList - Se retornan elementos")]
        public void Numbers_GetList()
        {
            // Arrange
            var nums = new Data.Numbers();

            // Act
            var result = nums.GetList();

            // Assert
            Assert.True(result != null, "No hay colección.");
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        //[InlineData(8)]
        public void Number_IsOdd(int num)
        {
            // Arrange
            var nums = new Data.Numbers();

            // Act
            var result = nums.IsOdd(num);

            // Assert
            Assert.True(result, "El número no fué reconocido como impar.");
        }
    }
}
