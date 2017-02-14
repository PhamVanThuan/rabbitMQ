using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace RabbitMQ.Entitites
{
    public class Consumer
    {
        private readonly IModel Model;
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
            Model = Connection.CreateModel();
            Model.QueueDeclare(QueueName, true, false, false, null);
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
            var consumer = new QueueingBasicConsumer(Model);
            String consumerTag = Model.BasicConsume(QueueName, false, consumer);
            while (isConsuming)
            {
                try
                {
                    Client.Events.BasicDeliverEventArgs e = consumer.Queue.Dequeue();
                    IBasicProperties props = e.BasicProperties;
                    byte[] body = e.Body;
                    // ... process the message
                    OnMessageReceived(body);
                    Model.BasicAck(e.DeliveryTag, false);
                }
                catch (OperationInterruptedException ex)
                {
                    // The consumer was removed, either through
                    // channel or connection closure, or through the
                    // action of IModel.BasicCancel().
                    break;
                }
            }
        }

        public void Dispose()
        {
            isConsuming = false;
            if (Connection != null)
                Connection.Close();
            if (Model != null)
                Model.Abort();
        }
    }
}