using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Entitites;

namespace WorkQueues
{
    class QueueWriter
    {
        private int MessageNo { get; set; }
        private Producer Producer { get; set; }

        public QueueWriter(int messageNo, string queueName)
        {
            MessageNo = messageNo;
            Producer = new Producer(WorkQueuesConfig.Host, queueName);
        }

        public bool writeOnQueue()
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
