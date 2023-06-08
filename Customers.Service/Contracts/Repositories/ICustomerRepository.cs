using Common.Data;
using Customers.Service.Models;
using MongoDB.Driver;

namespace Customers.Service.Contracts.Repositories;

public interface ICustomerRepository
{
    public PaginatedCollection<Customer> GetCustomersPaged(FilterDefinition<Customer> filter, int pageNumber, int pageSize);

    public List<Customer> GetAllCustomers();

    public Customer? GetCustomerById(string id);

    public Customer CreateCustomer(Customer customer);

    public Customer UpdateCustomer(Customer customer);

    public void DeleteCustomer(string id);
}