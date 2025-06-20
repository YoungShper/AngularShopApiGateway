namespace Shop.ProductService.Models;

public class PagedResponse<T>
{
    public List<T> Items { get; set; }
    public int? TotalPages { get; set; } = null;
}