//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Services;
//using Services.ViewModels;
//using System.Threading.Tasks;

//namespace MyBankApp.Pages
//{
//    public class CustomerDetailsModel : PageModel
//    {
//        private readonly CustomerService _customerService;


//        public CustomerDetailsModel(CustomerService customerService)
//        {
//            _customerService = customerService;
//        }

//        public CustomerDetailsViewModel CustomerDetails { get; set; }

//        public async Task<IActionResult> OnGetAsync(int id)
//        {
//            CustomerDetails = await _customerService.GetCustomerDetailsAsync(id);

//            if (CustomerDetails == null)
//            {
//                return NotFound();
//            }

//            return Page();
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Linq;
using Services;
using MyBankApp.ViewModels;
using Services.ViewModels;

namespace MyBankApp.Pages
{
    public class CustomerDetailsModel : PageModel
    {
        private readonly CustomerService _customerService;

        public CustomerDetailsModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public Services.ViewModels.CustomerDetailsViewModel CustomerDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CustomerDetails = await _customerService.GetCustomerDetailsAsync(id);

            if (CustomerDetails == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}