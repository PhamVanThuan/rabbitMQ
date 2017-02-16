using System;
using System.Text;
using RabbitMQ.Entitites;

namespace HelloWorld
{
    class QueueWriter
    {
        public QueueWriter(int messageNo, string queueName)
        {
            MessageNo = messageNo;
            QueueName = queueName;
        }

        private int MessageNo { get; set; }
        private string QueueName { get; set; }

        public bool WriteOnQueue()
        {
            for (var i = 0; i < MessageNo; i++)
            {
                var message = Guid.NewGuid().ToString();
                var body = Encoding.UTF8.GetBytes(message);
                var producer = new Producer("localhost", QueueName);
                producer.SendMessage(body);

                Console.WriteLine(" [S] {0}", message);
            }

            return true;
        }
    }
}
