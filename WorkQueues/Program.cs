using System;
using System.Threading.Tasks;

namespace WorkQueues
{
    class Program
    {
        private static void Main()
        {
            var sendingDone = true;
            var receivingDone = true;

            Parallel.Invoke(
                () => { sendingDone = new QueueWriter(WorkQueuesConfig.MessageNo, WorkQueuesConfig.QueueName).writeOnQueue(); },
                () => { receivingDone = new QueueReader("1st", WorkQueuesConfig.MessageNo, WorkQueuesConfig.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("2nd", WorkQueuesConfig.MessageNo, WorkQueuesConfig.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("3rd", WorkQueuesConfig.MessageNo, WorkQueuesConfig.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("4th", WorkQueuesConfig.MessageNo, WorkQueuesConfig.QueueName).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("5th", WorkQueuesConfig.MessageNo, WorkQueuesConfig.QueueName).ReadFromQueue(); }
                );

            while (sendingDone || receivingDone) { }

            Console.WriteLine("");
            Console.WriteLine("Job Complete!");
            Console.ReadLine();
        }
    }
}
           Console.WriteLine("Job Complete!");
            Console.ReadLine();
        }
    }
}
