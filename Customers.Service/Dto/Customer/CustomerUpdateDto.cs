using System.ComponentModel.DataAnnotations;

namespace Customers.Service.Dto.Customer;

public class CustomerUpdateDto
{
    [Required] 
    public string Id { get; set; } = null!;
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Email { get; set; } = null!;
    
    [Required]
    public string Phone { get; set; } = null!;
}