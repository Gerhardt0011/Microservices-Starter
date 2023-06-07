using Common.Events.Customers;

namespace Customers.Service.Events;

public class CustomerUpdated : ICustomerUpdated
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}