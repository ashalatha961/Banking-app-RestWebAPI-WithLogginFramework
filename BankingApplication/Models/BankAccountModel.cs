using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BankingApplication.Models
{
    public class BankingAccountModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength =3, ErrorMessage = "AccountHolderName must be between 3 to 30 characters.")]
        public string? AccountHolderName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value.")]
        public decimal Balance { get; set; }

    }

}
