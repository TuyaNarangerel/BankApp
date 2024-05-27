using System;
using System.ComponentModel.DataAnnotations;

namespace MyBankApp.Models
{
    public class TransactionViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Kontonummer måste vara större än noll")]
        public int AccountId { get; set; }
       

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; } // Deposit, Withdraw, Transfer

        public DateTime Date { get; set; }
    }
}
