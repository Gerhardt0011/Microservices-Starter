using AutoMapper;
using Customers.Service.Dto.Customer;
using Customers.Service.Models;
using Customers.Service.Events;

namespace Customers.Service.Profiles;

public class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<Customer, CustomerReadDto>();
        CreateMap<CustomerUpdateDto, Customer>();
        CreateMap<CustomerCreateDto, Customer>();
        
        // Event Mappings
        CreateMap<Customer, CustomerCreated>();
        CreateMap<Customer, CustomerUpdated>();
    }
}