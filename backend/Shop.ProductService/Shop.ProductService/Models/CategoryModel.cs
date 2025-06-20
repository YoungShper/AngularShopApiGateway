namespace Shop.ProductService.Models;

public class CategoryModel
{
    public Guid Id { get; set; }                  
    public string Name { get; set; }               
    public string ClassName { get; set; } = "Categories";
}