using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            var sendingDone = true;
            var receivingDone = true;

            Parallel.Invoke(
                () => { sendingDone = new QueueWriter(Config.MessageNo).WriteOnQueue(); },
                () => { receivingDone = new QueueReader("1st", Config.MessageNo).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("2nd", Config.MessageNo).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("3rd", Config.MessageNo).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("4th", Config.MessageNo).ReadFromQueue(); },
                () => { receivingDone = new QueueReader("5th", Config.MessageNo).ReadFromQueue(); }
                );

            //while (sendingDone || receivingDone) { }

            //Console.WriteLine("");
            //Console.WriteLine("Job Complete!");
            Console.ReadLine();
        }
    }
}
