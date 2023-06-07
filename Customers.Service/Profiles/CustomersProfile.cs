using AutoMapper;
using Customers.Service.Dto.Customer;
using Customers.Service.Models;
using Customers.Service.Events;
using Customers.Service.Contracts.Requests;

namespace Customers.Service.Profiles;

public class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<Customer, CustomerReadDto>();
        CreateMap<UpdateCustomerRequest, Customer>();
        CreateMap<CreateCustomerRequest, Customer>();

        // Event Mappings
        CreateMap<Customer, CustomerCreated>();
        CreateMap<Customer, CustomerUpdated>();
    }
}