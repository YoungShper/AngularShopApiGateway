namespace Shop.ApiGateWay.Interfaces;

public interface IForwardingService
{
    Task<string> GetAsync(string url, CancellationToken ct);
    Task<string> GetByIdAsync(string url, string id, CancellationToken ct);
    Task<string> PostAsync(string url, HttpRequest request, CancellationToken ct);
    Task<string> PutAsync(string url, HttpRequest request, CancellationToken ct);
    Task<string> DeleteAsync(string url, string id, CancellationToken ct);
}