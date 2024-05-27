using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.ViewModels;
using System.Threading.Tasks;

namespace MyBankApp.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal TotalBalance { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
    }
}