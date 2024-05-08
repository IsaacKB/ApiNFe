using Models;

namespace pocApiSefaz.Repositories.Interfaces
{
    public interface IRabbitMQRepository
    {
        void sendMessage(string message);
    }
}
