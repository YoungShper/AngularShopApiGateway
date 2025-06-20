namespace Shop.ApiGateWay.Models;

public class CartDataModel
{
    public Guid? CartId { get; set; }
    public bool IsActual { get; set; }
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}