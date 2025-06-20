using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Shop.ArticleService.Interfaces
{
    public interface IDBService
    {
        IDbConnection CreateConnection();
    }
}