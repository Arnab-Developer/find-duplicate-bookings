using FindDuplicateBookings.Lib.Settings;
using Xunit;

namespace FindDuplicateBookings.ConsoleAppTests.Settings
{
    public class ExecutionSettingsTests
    {
        [Fact]
        public void ExecutionSettingsModelTest()
        {
            // Arrange.
            ExecutionSettings executionSettings = new()
            {
                ExecutionFrequency = 300
            };

            // Assert.
            Assert.Equal(300, executionSettings.ExecutionFrequency);
        }
    }
}
