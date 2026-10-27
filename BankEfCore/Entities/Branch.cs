using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankEfCore.Entities;

public class Branch
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]

    public int Code { get; set; }
    [MaxLength(50)]
    public String Address { get; set; } = null!;
    [MaxLength(20)]
    public String Name { get; set; } = null!;
    [Phone]
    public String PhoneNumber { get; set; } = null!;
    // 1 - 1  with Manager
    public Manager Manager { get; set; } = null!;
    public int ManagerId { get; set; }

    // 1 - M Branch and Account
    public ICollection<Account> Accounts { get; set; } = [];
}
