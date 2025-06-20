namespace Shop.CartService.Models
{
    public class StatusModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; } = "Status";
    }
}