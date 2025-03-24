using GaEpd.AppLibrary.Apis;

namespace AppLibrary.Tests.ApiUtilityTests;

public class UrlCombineTests
{
    private const string BaseUri = "https://localhost";
    private const string BaseUriWithTrailingSlash = "https://localhost/";
    private const string Path = "path";
    private const string PathWithLeadingSlash = "/path";

    private const string ExpectedResultWithoutPath = "https://localhost";
    private const string ExpectedResultWithPath = "https://localhost/path";

    [TestCase(BaseUri, Path)]
    [TestCase(BaseUriWithTrailingSlash, Path)]
    [TestCase(BaseUri, PathWithLeadingSlash)]
    [TestCase(BaseUriWithTrailingSlash, PathWithLeadingSlash)]
    public void UrlCombine_WithValidSegments_ReturnsCombinedUri(string baseUri, string? relativeUri)
    {
        ApiUtilities.UrlCombine(new Uri(baseUri), relativeUri)
            .Should().Be(new Uri(ExpectedResultWithPath));
    }

    [TestCase(BaseUri, Path)]
    [TestCase(BaseUriWithTrailingSlash, Path)]
    [TestCase(BaseUri, PathWithLeadingSlash)]
    [TestCase(BaseUriWithTrailingSlash, PathWithLeadingSlash)]
    public void UrlCombine_WithValidSegments_AsStrings_ReturnsCombinedUri(string baseUri, string? relativeUri)
    {
        ApiUtilities.UrlCombine(baseUri, relativeUri)
            .Should().Be(new Uri(ExpectedResultWithPath));
    }

    [TestCase(BaseUri)]
    [TestCase(BaseUriWithTrailingSlash)]
    public void UrlCombine_WithNullPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UrlCombine(new Uri(baseUri), null)
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(BaseUri)]
    [TestCase(BaseUriWithTrailingSlash)]
    public void UrlCombine_WithStringBase_AndNullPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UrlCombine(baseUri, null)
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(BaseUri)]
    [TestCase(BaseUriWithTrailingSlash)]
    public void UrlCombine_WithEmptyPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UrlCombine(new Uri(baseUri), "")
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(BaseUri)]
    [TestCase(BaseUriWithTrailingSlash)]
    public void UrlCombine_WithStringBase_AndEmptyPath_ReturnsBaseUri(string baseUri)
    {
        ApiUtilities.UrlCombine(baseUri, "")
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [Test]
    public void UrlCombine_WithEmptyBase_Throws()
    {
        var func = () => ApiUtilities.UrlCombine("", null);
        func.Should().Throw<ArgumentException>();
    }

    [Test]
    public void UrlCombine_WithInvalidBase_Throws()
    {
        var func = () => ApiUtilities.UrlCombine("invalid", null);
        func.Should().Throw<UriFormatException>();
    }

    [Test]
    public void UrlCombine_WithRelativeBase_Throws()
    {
        var func = () => ApiUtilities.UrlCombine(new Uri("localhost", UriKind.Relative), null);
        func.Should().Throw<ArgumentOutOfRangeException>();
    }
}
