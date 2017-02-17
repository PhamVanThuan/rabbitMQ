using System;
using System.Text;
using RabbitMQ.Entitites;

namespace PublishSubscribe
{
    class QueueReader
    {
        public QueueReader(string readerIdentifier, int messageNo)
        {
            ReaderIdentifier = readerIdentifier;
            MessageNo = messageNo;
            Consumer = new ConsumerExchange(Config.Host, Config.ExchangeName);
            Consumer.OnMessageReceived += HandleMessage;
        }

        private ConsumerExchange Consumer { get; set; }
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
