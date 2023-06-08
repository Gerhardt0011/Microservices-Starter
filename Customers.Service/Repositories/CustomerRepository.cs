using Customers.Service.Contracts.Repositories;
using Customers.Service.Models;
using MongoDB.Driver;
using Common.Data;

namespace Customers.Service.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customers;

    public CustomerRepository(IMongoCollection<Customer> customers)
    {
        _customers = customers;
    }

    public PaginatedCollection<Customer> GetCustomersPaged(FilterDefinition<Customer> filter, int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var totalCount = _customers.CountDocuments(filter);
        var customers = _customers.Find(filter)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Limit(pageSize)
                                  .ToList();

        return new PaginatedCollection<Customer>(customers, totalCount, pageNumber, pageSize);
    }

    public List<Customer> GetAllCustomers()
    {
        return _customers.Find(customer => true).ToList();
    }

    public Customer? GetCustomerById(string id)
    {
        return _customers.Find(customer => customer.Id == id).FirstOrDefault();
    }

    public Customer CreateCustomer(Customer customer)
    {
        _customers.InsertOne(customer);

        return customer;
    }

    public Customer UpdateCustomer(Customer customer)
    {
        _customers.ReplaceOne(c => c.Id == customer.Id, customer);

        return customer;
    }

    public void DeleteCustomer(string id)
    {
        _customers.DeleteOne(customer => customer.Id == id);
    }
}