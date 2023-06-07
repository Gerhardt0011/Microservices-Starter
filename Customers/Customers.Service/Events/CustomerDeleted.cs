using Common.Events.Customers;

namespace Customers.Service.Events;

public class CustomerDeleted : ICustomerDeleted
{
    public string Id { get; set; } = null!;
}