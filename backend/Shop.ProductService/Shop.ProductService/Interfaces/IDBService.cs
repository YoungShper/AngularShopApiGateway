using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Shop.ProductService.Interfaces;

public interface IDBService
{
    IDbConnection CreateConnection();
}