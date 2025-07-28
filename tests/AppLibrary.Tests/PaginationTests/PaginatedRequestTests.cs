using GaEpd.AppLibrary.Pagination;

namespace AppLibrary.Tests.PaginationTests;

public class PaginatedRequestTests
{
    [Test]
    public void ReturnsWithPagingGivenValidInputs()
    {
        // Arrange
        const int pageNumber = 2;
        const int pageSize = 10;
        const string sorting = "Note";

        // Act
        var pagination = new PaginatedRequest(pageNumber, pageSize, sorting);

        // Assert
        using var scope = new AssertionScope();
        pagination.PageSize.Should().Be(pageSize);
        pagination.PageNumber.Should().Be(pageNumber);
        pagination.Sorting.Should().Be(sorting);
        pagination.Skip.Should().Be((pageNumber - 1) * pageSize);
        pagination.Take.Should().Be(pageSize);
    }

    [Test]
    public void ThrowsExceptionGivenZeroPageSize()
    {
        // Arrange
        const int pageNumber = 2;
        const int pageSize = 0;

        // Act
        var action = () => new PaginatedRequest(pageNumber, pageSize, "_");

        // Assert
        action.Should().Throw<ArgumentException>().And.ParamName.Should().Be(nameof(pageSize));
    }

    [Test]
    public void ThrowsExceptionGivenZeroPageNumber()
    {
        // Arrange
        const int pageNumber = 0;
        const int pageSize = 20;

        // Act
        var action = () => new PaginatedRequest(pageNumber, pageSize, "_");

        // Assert
        action.Should().Throw<ArgumentException>().And.ParamName.Should().Be(nameof(pageNumber));
    }

    [Test]
    public void ThrowsExceptionGivenNullSorting()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 20;

        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var action = () => new PaginatedRequest(pageNumber, pageSize, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Assert
        action.Should().Throw<ArgumentException>().And.ParamName.Should().Be("sorting");
    }

    [Test]
    public void ThrowsExceptionGivenEmptySorting()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 20;

        // Act
        var action = () => new PaginatedRequest(pageNumber, pageSize, string.Empty);

        // Assert
        action.Should().Throw<ArgumentException>().And.ParamName.Should().Be("sorting");
    }
}
