using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankEfCore.Entities;

public class Transaction
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int TransactionNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    [MaxLength(250)]
    public string? Note { get; set; }
    [Precision(15, 2)]
    public decimal Amount { get; set; }

    public TransactionType TransactionType { get; set; }
    //1- M Account with Transaction
    public Account Account { get; set; } = null!;
    public int AccountNumber { get; set; }
}
public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}