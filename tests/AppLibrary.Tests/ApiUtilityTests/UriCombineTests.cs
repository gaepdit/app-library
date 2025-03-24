using GaEpd.AppLibrary.Apis;

namespace AppLibrary.Tests.ApiUtilityTests;

public class UrlCombineTests
{
    private const string BaseUrl = "https://localhost";
    private const string BaseUrlWithTrailingSlash = "https://localhost/";
    private const string Path = "path";
    private const string PathWithLeadingSlash = "/path";

    private const string ExpectedResultWithoutPath = "https://localhost";
    private const string ExpectedResultWithPath = "https://localhost/path";

    [TestCase(BaseUrl, Path)]
    [TestCase(BaseUrlWithTrailingSlash, Path)]
    [TestCase(BaseUrl, PathWithLeadingSlash)]
    [TestCase(BaseUrlWithTrailingSlash, PathWithLeadingSlash)]
    public void UrlCombine_WithValidSegments_ReturnsCombinedUrl(string baseUrl, string? relativeUrl)
    {
        ApiUtilities.UrlCombine(new Uri(baseUrl), relativeUrl)
            .Should().Be(new Uri(ExpectedResultWithPath));
    }

    [TestCase(BaseUrl, Path)]
    [TestCase(BaseUrlWithTrailingSlash, Path)]
    [TestCase(BaseUrl, PathWithLeadingSlash)]
    [TestCase(BaseUrlWithTrailingSlash, PathWithLeadingSlash)]
    public void UrlCombine_WithValidSegments_AsStrings_ReturnsCombinedUrl(string baseUrl, string? relativeUrl)
    {
        ApiUtilities.UrlCombine(baseUrl, relativeUrl)
            .Should().Be(new Uri(ExpectedResultWithPath));
    }

    [TestCase(BaseUrl)]
    [TestCase(BaseUrlWithTrailingSlash)]
    public void UrlCombine_WithNullPath_ReturnsBaseUrl(string baseUrl)
    {
        ApiUtilities.UrlCombine(new Uri(baseUrl), null)
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(BaseUrl)]
    [TestCase(BaseUrlWithTrailingSlash)]
    public void UrlCombine_WithStringBase_AndNullPath_ReturnsBaseUrl(string baseUrl)
    {
        ApiUtilities.UrlCombine(baseUrl, null)
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(BaseUrl)]
    [TestCase(BaseUrlWithTrailingSlash)]
    public void UrlCombine_WithEmptyPath_ReturnsBaseUrl(string baseUrl)
    {
        ApiUtilities.UrlCombine(new Uri(baseUrl), "")
            .Should().Be(new Uri(ExpectedResultWithoutPath));
    }

    [TestCase(BaseUrl)]
    [TestCase(BaseUrlWithTrailingSlash)]
    public void UrlCombine_WithStringBase_AndEmptyPath_ReturnsBaseUrl(string baseUrl)
    {
        ApiUtilities.UrlCombine(baseUrl, "")
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
