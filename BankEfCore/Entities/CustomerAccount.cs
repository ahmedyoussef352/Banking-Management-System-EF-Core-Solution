using System.ComponentModel.DataAnnotations;

namespace BankEfCore.Entities;

//M - M Customer And Account
public class CustomerAccount
{
    // Customer
    public Customer Customer { get; set; } = null!;
    public int CustomerId { get; set; }
    //Account
    public Account Account { get; set; } = null!;
    public int AccountNumber { get; set; }

    // Have
    public DateTime OwnershipStartDate { get; set; }

    [MaxLength(50)]
    public OwnershipType OwnershipType { get; set; }

    [MaxLength(50)]
    public string AccountStatus { get; set; } = null!;
}
public enum OwnershipType
{
    Primary = 1,
    CoHolder = 2
}