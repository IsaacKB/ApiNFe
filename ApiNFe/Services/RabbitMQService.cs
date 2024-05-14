using System.Text;
using ApiNFe.Services.Interfaces;
using RabbitMQ.Client;

namespace ApiNFe.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private ConnectionFactory factory { get; set; }

        public RabbitMQService()
        {
            factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        public void sendMessage(string message)
        {
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "Fila1",     // Nome da fila
                        durable: false,     // Persistir os dados
                        exclusive: false,   // Uma conexão por vez
                        autoDelete: false,  // Apagar fila quando inativa
                        arguments: null
                    );

                    var bodyMessage = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "Fila1", // Identifica a fila
                        basicProperties: null,
                        body: bodyMessage // Body com a mensagem a ser enviada
                    );

                    Console.Write("Mensagem enviada com sucesso!");
                }
            }
        }
    }
}
