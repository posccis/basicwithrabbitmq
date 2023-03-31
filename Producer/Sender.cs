using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost"};
            using (IConnection connection = factory.CreateConnection()) 
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);
                string message = "Getting started with .Net Core RabbitMQ";
                byte[] body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", "BasicTest", null, body);
                Console.WriteLine("Sent message {0}...", message);
            }
            Console.WriteLine("Press [enter] to exit the Sender App...");
            Console.ReadLine();
        }
    }
}
