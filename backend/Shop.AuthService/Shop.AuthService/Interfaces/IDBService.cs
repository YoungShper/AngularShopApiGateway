using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Shop.AuthService.Interfaces
{
    public interface IDBService
    {
        IDbConnection CreateConnection();
    }
}