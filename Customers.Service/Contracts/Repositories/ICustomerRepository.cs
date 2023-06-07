using Customers.Service.Models;

namespace Customers.Service.Contracts.Repositories;

public interface ICustomerRepository
{
    public List<Customer> GetAllCustomers();
    
    public Customer? GetCustomerById(string id);
    
    public Customer CreateCustomer(Customer customer);
    
    public Customer UpdateCustomer(Customer customer);
    
    public void DeleteCustomer(string id);
}