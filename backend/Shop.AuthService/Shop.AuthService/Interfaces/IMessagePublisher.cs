namespace Shop.AuthService.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(string queueName, string message, CancellationToken ct);
    }
}
