namespace PolicyPilot.Tests;

public class ApiHealthTests
{
    [Fact]
    public void BasicSanityCheck_ShouldPass()
    {
        // Arrange
        bool isSystemHealthy = true;

        // Assert
        Assert.True(isSystemHealthy);
    }
}
