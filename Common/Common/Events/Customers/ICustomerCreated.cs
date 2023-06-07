namespace Common.Events.Customers;

public interface ICustomerCreated
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}