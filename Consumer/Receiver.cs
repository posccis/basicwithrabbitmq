using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost"};
            using ( IConnection connection= factory.CreateConnection())
                using(IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false,null) ;
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    string message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine("Received message {0}...", message);
                };
                channel.BasicConsume("BasicTest", true, consumer);
                Console.WriteLine("Press [enter] to exit the Consumer.");
                Console.ReadLine();
            }
        }
    }
}
