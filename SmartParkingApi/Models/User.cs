using System.ComponentModel.DataAnnotations;
namespace SmartParkingApi.Models;

public class User
{
  [Key]
  public int Id { get; set; }
  [Required]
  public required string Username { get; set; }
  [Required]
  public string PasswordHash { get; set; } = string.Empty;

  public string Role { get; set; } = "Staff";
}