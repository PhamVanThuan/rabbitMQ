using System;
using RabbitMQ.Client;

namespace RabbitMQ.Entitites
{
    public class Producer : IDisposable
    {
        private readonly IModel Channel;
        private readonly IConnection Connection;
        private readonly string QueueName;

        public Producer(string hostName, string queueName)
        {
            QueueName = queueName;
            var connectionFactory = new ConnectionFactory {HostName = hostName};
            Connection = connectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(QueueName, true, false, false, null);
        }

        public void Dispose()
        {
            if (Connection != null && Connection.IsOpen)
                Connection.Close();
            if (Channel != null)
                Channel.Abort();
        }

        public void SendMessage(byte[] message)
        {
            var basicProperties = Channel.CreateBasicProperties();
            Channel.BasicPublish("", QueueName, basicProperties, message);
        }
    }
}