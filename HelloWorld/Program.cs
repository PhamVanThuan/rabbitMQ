using System;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        private static void Main()
        {
            var sendingDone = true;
            var receivingDone = true;

            Parallel.Invoke(
                () => { sendingDone = new QueueWriter(Config.MessageNo, Config.QueueName).WriteOnQueue(); },
                () => { receivingDone = new QueueReader(Config.MessageNo, Config.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader(Config.MessageNo, Config.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader(Config.MessageNo, Config.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader(Config.MessageNo, Config.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader(Config.MessageNo, Config.QueueName).ReadFromQueue(); }
                );

            while (sendingDone || receivingDone) { }

            Console.WriteLine("");
            Console.WriteLine("Job Complete!");
            Console.ReadLine();
        }
    }
}
