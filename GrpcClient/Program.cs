// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcClient.Protos;
using GrpcServer.Protos;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5041");
            var client = new Greeter.GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "Shinei Nouzen" });

            Console.WriteLine("Greeting: " + reply.Message);

            var customerClient = new Customer.CustomerClient(channel);

            var customerReply = customerClient.GetCustomerInfo(new CustomerLookUpModel { UserId = 3 });
            Console.WriteLine("Customer Info: " + customerReply.FirstName + " " + customerReply.LastName + " " 
            + customerReply.Email + " " + customerReply.PhoneNumber + " " + customerReply.Age + " " + customerReply.IsActive);

            Console.ReadLine();
        }
    }
}