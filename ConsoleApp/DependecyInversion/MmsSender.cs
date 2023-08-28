using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DependecyInversion
{
    internal class MmsSender : IMessage
    {
        public string Number { get; set; }
        public byte[] Content { get; set; }

        public void Send()
        {
            SendMms();
        }

        public void SendMms()
        {
            Console.WriteLine("Sending mms");
        }
    }
}
