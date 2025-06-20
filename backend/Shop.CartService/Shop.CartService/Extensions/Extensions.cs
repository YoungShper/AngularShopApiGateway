using System.Collections.Specialized;
using Shop.CartService.Models;

namespace Shop.CartService.Extensions
{
    public static class Extensions
    {
        public static string ToCartFilters(this NameValueCollection query)
        {
            var result = string.Empty;
            var categories = new string[]{};
            var user_ids = new string[]{};
        
            if (!string.IsNullOrEmpty(query.Get("ids")))
            {
                var data = query.Get("ids");
                user_ids = data.Split(',');
            }
        
        
            result += user_ids.Any() ? $" AND user_id IN ('{string.Join("','", user_ids)}')" : "";
        
            return result;
        }
    }
}