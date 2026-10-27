using BankEfCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Account
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int AccountNumber { get; set; }
    [Precision(10, 2)]
    public decimal CurrentBalance { get; set; }
    public AccountType AccountType { get; set; }
    public DateTime OpeningDate { get; set; }
    // 1 - M Account And Transaction
    public ICollection<Transaction> Transactions { get; set; } = [];

    // 1 - M Branch and Account
    public Branch Branch { get; set; } = null!;
    public int BranchCode { get; set; }
    // m-m 
    public ICollection<CustomerAccount> CustomerAccounts { get; set; } = [];
}

public enum AccountType
{
    Current,
    Savings
}