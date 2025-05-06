using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services;

public class CustomerService : Customer.CustomerBase
{
    private ILogger<CustomerService> _logger;

    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookUpModel request,
        ServerCallContext context)
    {
        CustomerModel customer = new CustomerModel();

        if (request.UserId == 1)
        {
            customer.UserId = 1;
            customer.FirstName = "Shinei";
            customer.LastName = "Nouzen";
            customer.Email = "hello@123.com";
            customer.PhoneNumber = "123-456-7890";
            customer.Age = 20;
            customer.IsActive = true;
        }
        else if (request.UserId == 2)
        {
            customer.UserId = 2;
            customer.FirstName = "RAM";
            customer.LastName = "Nouzen";
            customer.Email = "hsa";
            customer.PhoneNumber = "123-456-7890";
            customer.Age = 20;
            customer.IsActive = true;
        }
        return Task.FromResult(customer);
    }

    public override async Task GetNewCustomers(NewCustomerRequest request,
        IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
    {
        List<CustomerModel> customers = new List<CustomerModel>
        {
            new CustomerModel
            {
                FirstName = "Shinei",
                LastName = "Nouzen",
                Email="@.com",
                PhoneNumber="123-456-7890",
                Age=20,
                IsActive=true
            },
            new CustomerModel
            {
                FirstName = "Shri",
                LastName = "Ram",
                Email="@.com",
                PhoneNumber="123-456-7890",
                Age=23,
                IsActive=true
            },
            new CustomerModel
            {
                FirstName = "Solo",
                LastName = "bro",
                Email="@.com",
                PhoneNumber="123-456-7890",
                Age=40,
                IsActive=true
            },

        };
        foreach (var customer in customers)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(customer);
        }
    }
}
