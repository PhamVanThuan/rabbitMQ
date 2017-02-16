using System;
using System.Text;
using RabbitMQ.Entitites;

namespace WorkQueues
{
    class QueueReader
    {
        public QueueReader(string readerIdentifier, int messageNo, string queueName)
        {
            ReaderIdentifier = readerIdentifier;
            MessageNo = messageNo;
            QueueName = queueName;
            Consumer = new Consumer(WorkQueuesConfig.Host, WorkQueuesConfig.QueueName);
            Consumer.OnMessageReceived += HandleMessage;
        }

        private Consumer Consumer { get; set; }
        private string ReaderIdentifier { get; set; }
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
            Console.WriteLine(" [ Receiving message by {0}: {1} ]", ReaderIdentifier, message);
        }
    }
}
