using Microsoft.Net.Http.Headers;
using Shop.ApiGateWay.Interfaces;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Shop.ApiGateWay;

public class ForwardingService : IForwardingService
{
    private readonly HttpClient _httpClient;

    public ForwardingService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string> GetAsync(string url, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync(url, ct);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetByIdAsync(string url, string id, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"{url}/{id}", ct);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PostAsync(string url, HttpRequest request, CancellationToken ct)
    {
        using var ms = new MemoryStream();
        await request.Body.CopyToAsync(ms, ct);
        ms.Position = 0;
        var content = new StreamContent(ms);
        var forwardRequest = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content
        };
        if (request.ContentType != null)
            forwardRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);

        if (request.Headers.ContentLength.HasValue)
            forwardRequest.Content.Headers.ContentLength = request.Headers.ContentLength;
        var response = await _httpClient.SendAsync(forwardRequest, ct);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> PutAsync(string url, HttpRequest request, CancellationToken ct)
    {
        using var ms = new MemoryStream();
        await request.Body.CopyToAsync(ms, ct);
        ms.Position = 0;
        var content = new StreamContent(ms);
        var forwardRequest = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = content
        };
        if (request.ContentType != null)
            forwardRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);

        if (request.Headers.ContentLength.HasValue)
            forwardRequest.Content.Headers.ContentLength = request.Headers.ContentLength;
        var response = await _httpClient.SendAsync(forwardRequest, ct);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> DeleteAsync(string url, string id, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"{url}/{id}", ct);
        return await response.Content.ReadAsStringAsync();
    }
}
