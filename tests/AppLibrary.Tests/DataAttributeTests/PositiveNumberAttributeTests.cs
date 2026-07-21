using GaEpd.AppLibrary.DataAttributes;

namespace AppLibrary.Tests.DataAttributeTests;

public class PositiveNumberAttributeTests
{
    [Test]
    public void PositiveDecimalAttribute_SetsRange()
    {
        // Arrange & Act
        var attribute = new PositiveDecimalAttribute();

        // Assert
        Convert.ToDecimal(attribute.Maximum).Should().Be(decimal.MaxValue);
        Convert.ToDecimal(attribute.Minimum).Should().Be(0);
    }

    [Test]
    public void PositiveShortAttribute_SetsRange()
    {
        // Arrange & Act
        var attribute = new PositiveShortAttribute();

        // Assert
        Convert.ToInt16(attribute.Maximum).Should().Be(short.MaxValue);
        Convert.ToInt16(attribute.Minimum).Should().Be(0);
    }
}
