using Models;

namespace ApiNFe.Services.Interfaces
{
    public interface IRabbitMQService
    {
        void sendMessage(string message);
    }
}
