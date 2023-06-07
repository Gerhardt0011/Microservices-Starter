namespace Common.Events.Customers;

public interface ICustomerUpdated
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}