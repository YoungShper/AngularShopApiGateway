using Microsoft.AspNetCore.Components;

namespace Shop.ProductService.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }                  
        public string Name { get; set; }               
        public string Description { get; set; }       
        public double Price { get; set; }             
        public int? Quantity { get; set; }
        public double? DiscountPrice { get; set; }
        public Guid? CategoryId { get; set; }
        public string ClassName { get; set; } = "products";
        public int Protein { get; set; }
        public int Fats { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
    }
}
