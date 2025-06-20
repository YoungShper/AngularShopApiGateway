namespace Shop.ProductService.Models;

public class UserProfileModel
{
    public string Goal { get; set; } // "gain", "loss", "maintain"
    public double Weight { get; set; }
    public double Height { get; set; }
    public int Age { get; set; }
}