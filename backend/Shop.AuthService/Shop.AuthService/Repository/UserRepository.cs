using System.Collections.Specialized;
using System.Data;
using Dapper;
using Shop.AuthService.Interfaces;
using Shop.AuthService.Models;

namespace Shop.AuthService.Repository
{
    public class UserRepository : IDbRepository<UserModel>
    {
        private IDBService _dBService;

        public UserRepository(IDBService dbService)
        {
            _dBService = dbService;
        }
        
        public async Task<List<UserModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken)
        {
            var maildata = filters["mail"] ?? "";
            
            var sqlQuery = $@"
                SELECT id AS Id,
                       last_name AS LastName,
                       first_name AS Name,
                       address AS Address,
                       postal_code AS PostalCode,
                       is_admin AS IsAdmin,
                       password AS Password,
                       mail AS Mail,
                       city AS City
                FROM userstable
                WHERE delete_state_code = 0 AND mail = @mail";
            using var connection = _dBService.CreateConnection();
            var result = await connection.QueryAsync<UserModel>(sqlQuery, new{ mail = maildata });
            return result.ToList();
        }

        public async Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var sqlQuery = $@"
                SELECT id AS Id,
                       last_name AS LastName,
                       first_name AS Name,
                       address AS Address,
                       postal_code AS PostalCode,
                       is_admin AS IsAdmin,
                       password AS Password,
                       mail AS Mail,
                       city AS City
                FROM userstable
                WHERE delete_state_code = 0 AND id = @ID";

            using var connection = _dBService.CreateConnection();
            IEnumerable<UserModel?> result = await connection.QueryAsync<UserModel>(sqlQuery, new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var sqlQuery = @"
                INSERT INTO userstable 
                (id, last_name, first_name, address, postal_code, is_admin, city, password, mail, delete_state_code)
                VALUES (@Id, @LastName, @Name, @Address, @PostalCode, @IsAdmin, @City, @Password, @Mail, 0)";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, 
                new
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    Name = user.Name,
                    Address = user.Address,
                    PostalCode = user.PostalCode,
                    Mail = user.Mail,
                    Password = user.Password,
                    IsAdmin = user.IsAdmin,
                    City = user.City
                });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var sqlQuery = @"
                UPDATE userstable 
                SET delete_state_code = 1 
                WHERE id = @Id";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<bool> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var sqlQuery = @"
                UPDATE userstable 
                SET last_name = @LastName,
                    first_name = @Name,
                    address = @Address,
                    postal_code = @PostalCode,
                    is_admin = @IsAdmin,
                    city = @City
                WHERE id = @Id AND delete_state_code = 0";

            using var connection = _dBService.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sqlQuery,
                new
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    Name = user.Name,
                    Address = user.Address,
                    PostalCode = user.PostalCode,
                    IsAdmin = user.IsAdmin,
                    City = user.City
                });
            return affectedRows > 0;
        }
    }
}