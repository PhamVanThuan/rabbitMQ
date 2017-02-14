using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Entitites;

namespace HelloWorld
{
    class QueueReader
    {
        public QueueReader(int messageNo, string queueName)
        {
            MessageNo = messageNo;
            QueueName = queueName;
            Consumer = new Consumer(Config.Host, Config.QueueName);
            Consumer.OnMessageReceived += HandleMessage;
        }

        private Consumer Consumer { get; set; }
        private int MessageNo { get; set; }
        private string QueueName { get; set; }

        public bool ReadFromQueue()
        {
            Consumer.StartConsuming();

            return true;
        }

        private void HandleMessage(byte[] queueMessage)
        {
            var message = Encoding.UTF8.GetString(queueMessage);
            Console.WriteLine(" [R] {0}", message);
        }
    }
}
