using Microsoft.AspNetCore.Components;

namespace Shop.ArticleService.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassName { get; set; } = "Articles";
    }
}
