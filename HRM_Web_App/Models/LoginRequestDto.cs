namespace HRM_Web_App.Models;
using System.ComponentModel.DataAnnotations;

public class LoginRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}