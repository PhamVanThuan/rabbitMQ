using System;
using RabbitMQ.Client;

namespace RabbitMQ.Entitites
{
    public class ProducerExchange : IDisposable
    {
        private readonly IModel Channel;
        private readonly IConnection Connection;
        private readonly String QueueName;
        private readonly String ExchangeName;

        public ProducerExchange(string hostName, String exchangeType, String exchangeName)
        {
            ExchangeName = exchangeName;

            var connectionFactory = new ConnectionFactory {HostName = hostName};
            Connection = connectionFactory.CreateConnection();
            Channel = Connection.CreateModel();

            Channel.ExchangeDeclare(ExchangeName, exchangeType);
            
            QueueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: String.Empty);
        }

        public void Dispose()
        {
            if (Connection != null && Connection.IsOpen)
                Connection.Close();
            if (Channel != null)
                Channel.Abort();
        }

        public void SendMessage(byte[] body)
        {
            //var basicProperties = Channel.CreateBasicProperties();
            Channel.BasicPublish(exchange: ExchangeName, routingKey: String.Empty, basicProperties: null, body: body);
        }
    }
}