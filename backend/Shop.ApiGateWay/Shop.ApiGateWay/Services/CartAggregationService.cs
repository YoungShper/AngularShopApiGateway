using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.ApiGateWay.Interfaces;
using Shop.ApiGateWay.Models;

namespace Shop.ApiGateWay;

public class CartAggregationService : ICartAggregationService
{
    IForwardingService _forwardingService;
    private readonly string _prodUrl;
    private readonly string _cartUrl;

    public CartAggregationService(IConfiguration configuration, IForwardingService forwardingService)
    {
        _forwardingService = forwardingService;
        _prodUrl = configuration.GetForwardingRoute("Products");
        _cartUrl = configuration.GetForwardingRoute("Cart");
    }
    
    public async Task<List<CartDataModel>> GetAllUserCartDataAsync(HttpRequest httpRequest, CancellationToken cancellationToken)
    {
        string queryString = string.Empty;
        var token = httpRequest.HttpContext.Request.Cookies["tasty-cookie"];
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "id");
        var userId = userIdClaim?.Value;
        
        var cartData = await _forwardingService.GetAsync($"{_cartUrl}?ids={string.Join(',', userId ?? "")}", cancellationToken);
        var cartList = JArray.Parse(cartData).Select(x => new CartModel
        {
            Id = (Guid?)x["id"],
            ProductId = (Guid)x["productId"],
            Quantity = (int)x["quantity"],
            CreatedOn = (DateTime?)x["createdOn"],
            UserId = (Guid)x["userId"],
            CartId = (Guid?)x["cartId"],
            StatusId = (Guid?)x["statusId"],
            IsActual = (bool)x["isActual"]
        }).ToList();
        
        var products = cartList.Select(x => x.ProductId).Distinct().ToList();
        
        var productData = await _forwardingService.GetAsync($"{_prodUrl}?ids={string.Join(',', products)}", cancellationToken);
        

        var productList = JToken.Parse(productData)["items"].Select( y => new ProductModel
        {
            Id = (Guid)y["id"],
            CartQuantity = (int)y["quantity"],
            Name = (string)y["name"],
            Description = (string)y["description"],
            DiscountPrice = (double?)y["discountPrice"],
            Price = (double)y["price"],
            CategoryID = (Guid?)y["categoryId"]
        }).ToList();

        var result = 
            (from cart in cartList
            join prod in productList on cart.ProductId equals prod.Id 
            group new {cart, prod} by cart.CartId into gr
            select new CartDataModel
            {
                IsActual = gr.FirstOrDefault().cart.IsActual,
                CartId = gr.Key,
                Products = gr.Select(x => x.prod).ToList()
            }).ToList();

        return result;

    }
}