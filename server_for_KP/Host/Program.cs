using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(server.Server)))
            {
                host.Open();
                Console.WriteLine("Хост работает");
                Console.ReadLine();
            }
        }
    }
}
