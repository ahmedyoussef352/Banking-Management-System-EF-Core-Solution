using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankEfCore.Entities;

[Index(nameof(Email), IsUnique = true)]

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]

    public int Id { get; set; }
    [MaxLength(50)]
    public String Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    [MaxLength(20)]
    public String FullName { get; set; } = null!;

    public String Email { get; set; } = null!;
    [Phone]
    public String PhoneNumber { get; set; } = null!;
    public long NationalId { get; set; }
    public CustomerType CustomerType { get; set; }
    // m-m 
    public ICollection<CustomerAccount> CustomerAccounts { get; set; }
        = [];
}
public enum CustomerType
{
    Normal,
    VIP,
    Corporate
}

