using System.Collections.Specialized;
using Shop.ProductService.Models;

namespace Shop.ProductService.Extensions;

public static class Extensions
{
    public static string ToProductFilters(this NameValueCollection query)
    {
        var result = string.Empty;
        var categories = new string[]{};
        var ids = new string[]{};
        
       

        if (!string.IsNullOrEmpty(query.Get("categories")))
        {
            var data = query.Get("categories");
            categories = data.Split(',');
        }
        
        if (!string.IsNullOrEmpty(query.Get("ids")))
        {
            var data = query.Get("ids");
            ids = data.Split(',');
        }
        
        
        result += ids.Any() ? $" AND id IN ('{string.Join("','", ids)}')" : "";
        result += !string.IsNullOrEmpty(query.Get("search")) ? $" AND name ILIKE '%{query.Get("search")}%'" : "";
        result += !string.IsNullOrEmpty(query.Get("priceFrom")) ? $" AND price >= {query.Get("priceFrom")}" : "";
        result += !string.IsNullOrEmpty(query.Get("priceTo")) ? $" AND price <= {query.Get("priceTo")}" : "";
        result += categories.Any() ? $" AND category_id IN ('{string.Join("','", categories)}')" : "";
        result += !string.IsNullOrEmpty(query.Get("discount")) ? $" AND discount_price is not null" : "";
        
        return result;
    }

    public static string GetPaginationQueryString(this NameValueCollection query, int pageSize = 10)
    {
        int? page = int.TryParse(query["page"], out var p) ? p : null;
        
        if (page.HasValue)
        {
            int offset = ((int)page - 1) * pageSize;
            return $@"OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }
        return string.Empty;
    }
}