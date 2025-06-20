using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Shop.CartService.Interfaces
{
    public interface IDBService
    {
        IDbConnection CreateConnection();
    }
}