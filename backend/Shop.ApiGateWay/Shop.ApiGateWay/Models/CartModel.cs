namespace Shop.ApiGateWay.Models;

public class CartModel
{
    public Guid? Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? CartId { get; set; }
    public int Quantity { get; set; }
    public Guid? StatusId { get; set; }
    public DateTime? CreatedOn { get; set;}
    public string ClassName { get; set; } = "Cart";
    public bool IsActual { get; set; }
}