using System;
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

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookUpModel request, ServerCallContext context)
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
}
