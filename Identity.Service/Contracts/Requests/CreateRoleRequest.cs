using System.ComponentModel.DataAnnotations;

namespace Identity.Service.Contracts.Requests;

public class CreateRoleRequest
{
    [Required] 
    public string Name { get; set; } = null!;
}