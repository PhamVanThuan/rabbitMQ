using System;
using System.Text;
using RabbitMQ.Entitites;

namespace PublishSubscribe
{
    class QueueWriter
    {
        private int MessageNo { get; set; }
        private ProducerExchange Producer { get; set; }

        public QueueWriter(int messageNo)
        {
            MessageNo = messageNo;
            Producer = new ProducerExchange(Config.Host, ExchangeType.Fanout, Config.ExchangeName);
        }

        public bool WriteOnQueue()
        {
            for (var i = 0; i < MessageNo; i++)
            {
                var message = Guid.NewGuid().ToString();
                var body = Encoding.UTF8.GetBytes(message);
                Producer.SendMessage(body);

                Console.WriteLine(" [ Sending message: {0} ]", message);
                //Thread.Sleep(1000);
            }

            return true;
        }
    }
}
