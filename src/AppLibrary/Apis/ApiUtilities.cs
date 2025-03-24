using System.Net.Http;
using System.Net.Http.Json;

namespace GaEpd.AppLibrary.Apis;

public static class ApiUtilities
{
    public static async Task<T?> FetchApiDataAsync<T>(this IHttpClientFactory httpClientFactory,
        Uri apiUrl, string endpointPath, string clientName = "")
    {
        using var client = httpClientFactory.CreateClient(clientName);
        using var response = await client.GetAsync(UriCombine(apiUrl, endpointPath)).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
    }

    public static Uri UriCombine(string baseUri, string? relativeUri) =>
        UriCombine(new Uri(Guard.NotNullOrWhiteSpace(baseUri)), relativeUri);

    public static Uri UriCombine(Uri baseUri, string? relativeUri)
    {
        Guard.NotNull(baseUri);
        if (!baseUri.IsAbsoluteUri) throw new ArgumentOutOfRangeException(nameof(baseUri));

        if (string.IsNullOrEmpty(relativeUri)) return baseUri;

        const char separator = '/';
        return new Uri(baseUri.ToString().TrimEnd(separator) + separator + relativeUri.TrimStart(separator));
    }
}
