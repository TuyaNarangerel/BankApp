using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyBankApp.ViewModels;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Linq;
using Services.ViewModels;

namespace MyBankApp.Pages
{
    public class CustomerSearchModel : PageModel
    {
        private readonly BankAppDataContext _context;
        private readonly ILogger<CustomerSearchModel> _logger;

        public CustomerSearchModel(BankAppDataContext context, ILogger<CustomerSearchModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public string SearchTerm { get; set; }
        public PagedResult<CustomerViewModel> Customers { get; set; }

        public async Task OnGetAsync(string searchTerm, int pageIndex = 1)
        {
            SearchTerm = searchTerm;

            _logger.LogInformation($"Searching for customers with term: {searchTerm}");

            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (int.TryParse(searchTerm, out int customerNumber))
                {
                    query = query.Where(c => c.CustomerId == customerNumber);
                }
                else
                {
                    query = query.Where(c => c.Givenname.Contains(searchTerm) || c.Surname.Contains(searchTerm) || c.City.Contains(searchTerm));
                }
            }

            Customers = await PagedResult<CustomerViewModel>.CreateAsync(query
                .Select(c => new CustomerViewModel
                {
                    CustomerId = c.CustomerId,
                    PersonalNumber = c.NationalId,
                    Name = c.Givenname + " " + c.Surname,
                    Address = c.Streetaddress,
                    City = c.City
                }), pageIndex, 50);
        }
    }
}