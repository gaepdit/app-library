using GaEpd.AppLibrary.Apis;

namespace AppLibrary.Tests.ExtensionTests;

public class UriCombineTests
{
    private const string Uri = "https://localhost";
    private const string UriWithTrailingSlash = "https://localhost/";
    private const string Path = "path";
    private const string PathWithLeadingSlash = "/path";

    private const string ExpectedResultWithoutPath = "https://localhost";
    private const string ExpectedResultWithPath = "https://localhost/path";

    [TestCase(Uri, Path)]
    [TestCase(UriWithTrailingSlash, Path)]
    [TestCase(Uri, PathWithLeadingSlash)]
    [TestCase(UriWithTrailingSlash, PathWithLeadingSlash)]
    public void UriCombine_WithValidSegments_ReturnsCombinedUri(string baseUri, string? relativeUri)
    {
        ApiUtilities.UriCombine(new Uri(baseUri), relativeUri)
            .Should().Be(new Uri(ExpectedResultWithPath));
    }

    [TestCase(Uri, Path)]
    [TestCase(UriWithTrailingSlash, Path)]
    [TestCase(Uri, PathWithLeadingSlash)]
    [TestCase(UriWithTrailingSlash, PathWithLeadingSlash)]
    public void UriCombine_WithValidSegments_AsStrings_ReturnsCombinedUri(string baseUri, string? relativeUri)
    {
        ApiUtilities.UriCombine(baseUri, relativeUri)
            .Should().Be(new Uri(ExpectedResultWithPath));
    }

    [TestCase(Uri)]
    [TestCase(UriWithTrailingSlash)]
    public void UriCombine_WithNullPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UriCombine(new Uri(baseUri), null)
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(Uri)]
    [TestCase(UriWithTrailingSlash)]
    public void UriCombine_WithStringBase_AndNullPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UriCombine(baseUri, null)
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(Uri)]
    [TestCase(UriWithTrailingSlash)]
    public void UriCombine_WithEmptyPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UriCombine(new Uri(baseUri), "")
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(Uri)]
    [TestCase(UriWithTrailingSlash)]
    public void UriCombine_WithStringBase_AndEmptyPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UriCombine(baseUri, "")
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [Test]
    public void UriCombine_WithEmptyBase_Throws()
    {
        var func = () => ApiUtilities.UriCombine("", null);
        func.Should().Throw<ArgumentException>();
    }

    [Test]
    public void UriCombine_WithInvalidBase_Throws()
    {
        var func = () => ApiUtilities.UriCombine("invalid", null);
        func.Should().Throw<UriFormatException>();
    }

    [Test]
    public void UriCombine_WithRelativeBase_Throws()
    {
        var func = () => ApiUtilities.UriCombine(new Uri("localhost", UriKind.Relative), null);
        func.Should().Throw<ArgumentOutOfRangeException>();
    }
}
