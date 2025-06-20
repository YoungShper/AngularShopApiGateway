using Shop.AuthService.Models;
using System.Collections.Specialized;

namespace Shop.AuthService.Interfaces
{
    public interface IUserManager
    {
        public Task<List<UserModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken);
        public Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> CreateAsync(UserModel item, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(UserModel item, CancellationToken cancellationToken);
    }
}
