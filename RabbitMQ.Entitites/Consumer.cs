using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace RabbitMQ.Entitites
{
    public class Consumer
    {
        private readonly IModel Channel;
        private readonly IConnection Connection;
        private readonly string QueueName;

        private bool isConsuming;

        // used to pass messages back to UI for processing
        public delegate void OnReceiveMessage(byte[] message);

        public event OnReceiveMessage OnMessageReceived;

        public Consumer(string hostName, string queueName)
        {
            QueueName = queueName;
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = hostName;
            Connection = connectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
            Channel.QueueDeclare(QueueName, true, false, false, null);
        }

        //internal delegate to run the queue consumer on a seperate thread
        private delegate void ConsumeDelegate();

        public void StartConsuming()
        {
            isConsuming = true;
            ConsumeDelegate c = Consume;
            c.BeginInvoke(null, null);
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(Channel);
            try
            {
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    OnMessageReceived(body);
                    var message = Encoding.UTF8.GetString(body);
                };
                Channel.BasicConsume(
                    queue: QueueName,
                    autoAck: true,
                    consumer: consumer);
            }
            catch (OperationInterruptedException ex)
            {
                // The consumer was removed, either through
                // channel or connection closure, or through the
                // action of IModel.BasicCancel().
                //break;
            }
        }

        public void Dispose()
        {
            isConsuming = false;
            if (Connection != null)
                Connection.Close();
            if (Channel != null)
                Channel.Abort();
        }
    }
}
