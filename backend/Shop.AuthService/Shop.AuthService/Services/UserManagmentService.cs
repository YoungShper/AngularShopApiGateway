using Shop.AuthService.Interfaces;
using Shop.AuthService.Models;
using System.Collections.Specialized;

namespace Shop.AuthService.Services
{
    public class UserManagmentService : IUserManager
    {
        private IDbRepository<UserModel> _repository;
        private IConfiguration _configuration;
        private IMessagePublisher _messagePublisher;

        public UserManagmentService(IDbRepository<UserModel> dbRepository, IConfiguration configuration, IMessagePublisher messagePublisher)
        {
            _configuration = configuration;
            _repository = dbRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task<bool> CreateAsync(UserModel item, CancellationToken cancellationToken)
        {
            return await _repository.CreateAsync(item, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync(id, cancellationToken);
            if (result)
            {
                _messagePublisher.Publish(_configuration["RabbitMQServer:Queues:DeleteProduct"] ?? throw new NullReferenceException(), id.ToString(), cancellationToken);
            }
            return result;
        }

        public async Task<List<UserModel>> GetAllAsync(NameValueCollection filters, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(filters, cancellationToken);
        }

        public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(UserModel item, CancellationToken cancellationToken)
        {
           return await _repository.UpdateAsync(item, cancellationToken);
        }
    }
}
