namespace Customers.Service.Dto.Customer;

public class PaginatedCustomerDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalPages { get; set; }
    public long TotalCount { get; set; }

    public IEnumerable<CustomerReadDto> Items { get; set; } = null!;
}