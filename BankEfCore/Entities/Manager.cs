using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankEfCore.Entities;

[Index(nameof(Email), IsUnique = true)]
public class Manager
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]

    public int Id { get; set; }
    [MaxLength(20)]
    public String FullName { get; set; } = null!;

    public String Email { get; set; } = null!;
    [Phone]
    public String PhoneNumber { get; set; } = null!;
    public DateTime HireDate { get; set; }

    // 1 - 1  with Branch
    public Branch Branch { get; set; } = null!;
}
