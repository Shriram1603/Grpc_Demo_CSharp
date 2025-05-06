// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient.Protos;
using GrpcServer.Protos;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5041");
            var client = new Greeter.GreeterClient(channel);

            var reply = client.SayHello(new HelloRequest { Name = "Shinei Nouzen" });

            Console.WriteLine("Greeting: " + reply.Message);

            var customerClient = new Customer.CustomerClient(channel);

            var customerReply = customerClient.GetCustomerInfo(new CustomerLookUpModel { UserId = 1 });
            Console.WriteLine("Customer Info: " + customerReply.FirstName + " " + customerReply.LastName + " "
            + customerReply.Email + " " + customerReply.PhoneNumber + " " + customerReply.Age + " " + customerReply.IsActive);

            Console.WriteLine();
            Console.WriteLine("New Customer List :");
            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var customer = call.ResponseStream.Current;
                    Console.WriteLine("\t New Customer: " + customer.FirstName + " " + customer.LastName
                        + " " + customer.Age);
                }
            }

            Console.ReadLine();
        }
    }
}