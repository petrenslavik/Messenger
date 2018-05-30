using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using test.ServiceReference1;

namespace test
{

    public class Implemented : IMessengerCallback
    {
        public void PingFromServer(DateTime now)
        {
            Console.WriteLine($"Ping from server: {(DateTime.Now - now).Milliseconds}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadKey();
            //DuplexChannelFactory<IMessengerCallback> factory = new DuplexChannelFactory<IMessengerCallback>(typeof(Implemented));
            //var channel = factory.CreateChannel();
            var context = new InstanceContext(new Implemented());
            var client = new ServiceReference1.MessengerClient(context);
            var res = client.PingToServer(DateTime.Now);
            var avg = new TimeSpan();
            Console.WriteLine($"Ping : {res.Milliseconds}");
            for (int i = 1; i <= 100; i++)
            {
                res = client.PingToServer(DateTime.Now);
                avg += res;
                Console.WriteLine($"Ping to server: {res.Milliseconds}");
            }
            Console.WriteLine($"Average ping : {avg.Milliseconds/101}");
            //Step 3: Closing the client gracefully closes the connection and cleans up resources.  
            client.Close();
        }
    }
}
