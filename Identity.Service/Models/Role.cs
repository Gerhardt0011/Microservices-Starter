using System.ComponentModel.DataAnnotations;

namespace Identity.Service.Models;

public class Role
{
    [Required]
    public string Name { get; set; } = null!;
}