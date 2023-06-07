using System.ComponentModel.DataAnnotations;

namespace Identity.Service.Contracts.Requests;

public class AddRoleRequest
{
    [Required]
    public string Name { get; set; } = null!;
}