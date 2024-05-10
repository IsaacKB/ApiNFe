using Models;

namespace pocApiSefaz.Services.Interfaces
{
    public interface IRabbitMQService
    {
        void sendMessage(string message);
    }
}
