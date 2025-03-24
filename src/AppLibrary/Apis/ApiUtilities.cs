using System.Net.Http;
using System.Net.Http.Json;

namespace GaEpd.AppLibrary.Apis;

public static class ApiUtilities
{
    /// <summary>
    /// Fetches JSON data from an API endpoint and deserializes it to the target type.
    /// This method does not handle authentication.
    /// </summary>
    /// <param name="httpClientFactory">The <see cref="T:System.Net.Http.IHttpClientFactory" /> available in your app.</param>
    /// <param name="apiUrl">The base URL for the API to connect to.</param>
    /// <param name="endpointPath">The relative path for the API endpoint to connect to.</param>
    /// <param name="clientName">A logical name for the <see cref="T:System.Net.Http.HttpClient" />.</param>
    /// <typeparam name="T">The target type to deserialize the API data to.</typeparam>
    /// <returns>API data deserialized to the target type.</returns>
    public static async Task<T?> FetchApiDataAsync<T>(this IHttpClientFactory httpClientFactory,
        Uri apiUrl, string endpointPath, string clientName = "")
    {
        using var client = httpClientFactory.CreateClient(clientName);
        using var response = await client.GetAsync(UrlCombine(apiUrl, endpointPath)).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
    }

    /// <summary>
    /// Combine a base URL and a relative URL path, correctly handling path separators.
    /// The base URL must be an absolute URL.
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="relativeUrl">The relative path.</param>
    /// <returns>The combined URL.</returns>
    public static Uri UrlCombine(string baseUrl, string? relativeUrl) =>
        UrlCombine(new Uri(Guard.NotNullOrWhiteSpace(baseUrl)), relativeUrl);

    /// <summary>
    /// Combine a base URL and a relative URL path, correctly handling path separators.
    /// The base URL must be an absolute URL.
    /// </summary>
    /// <param name="baseUrl">The base URL.</param>
    /// <param name="relativeUrl">The relative path.</param>
    /// <returns>The combined URL.</returns>
    public static Uri UrlCombine(Uri baseUrl, string? relativeUrl)
    {
        Guard.NotNull(baseUrl);
        if (!baseUrl.IsAbsoluteUri) throw new ArgumentOutOfRangeException(nameof(baseUrl));

        if (string.IsNullOrEmpty(relativeUrl)) return baseUrl;

        const char separator = '/';
        return new Uri(baseUrl.ToString().TrimEnd(separator) + separator + relativeUrl.TrimStart(separator));
    }
}
