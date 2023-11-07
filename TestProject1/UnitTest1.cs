


namespace TestProject1
{

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            [Fact]
            public void Index_ReturnsViewResult()
            {
                // Arrange
                var controller = new MyController();

                // Act
                var result = controller.Index();

                // Assert
                Assert.IsType<ViewResult>(result);
            }
        }
    }
}