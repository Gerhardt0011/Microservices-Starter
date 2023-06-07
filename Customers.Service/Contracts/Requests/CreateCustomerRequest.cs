using System.ComponentModel.DataAnnotations;

namespace Customers.Service.Contracts.Requests;

public class CreateCustomerRequest
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;
}