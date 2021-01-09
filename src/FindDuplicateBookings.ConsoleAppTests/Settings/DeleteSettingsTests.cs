using FindDuplicateBookings.Lib.Settings;
using Xunit;

namespace FindDuplicateBookings.ConsoleAppTests.Settings
{
    public class DeleteSettingsTests
    {
        [Fact]
        public void DeleteSettingsModelTest_True()
        {
            // Arrange.
            DeleteSettings deleteSettings = new()
            {
                IsDeleteEnable = "true"
            };

            // Assert.
            Assert.Equal("true", deleteSettings.IsDeleteEnable);
        }

        [Fact]
        public void DeleteSettingsModelTest_False()
        {
            // Arrange.
            DeleteSettings deleteSettings = new()
            {
                IsDeleteEnable = "false"
            };

            // Assert.
            Assert.NotEqual("true", deleteSettings.IsDeleteEnable);
        }
    }
}
