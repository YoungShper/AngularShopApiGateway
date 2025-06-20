namespace Shop.ApiGateWay.Models;

public class ProductModel
{
    public Guid Id { get; set; }                  
    public string Name { get; set; } 
    public string Description { get; set; }
    public int? CartQuantity { get; set; }
    public double Price { get; set; }             
    public int? Quantity { get; set; }
    public double? DiscountPrice { get; set; }
    public Guid? CategoryID { get; set; }
    public string ClassName { get; set; } = "products";
}