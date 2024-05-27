using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MyBankApp.Pages
{
    public class AccountDetailsModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public AccountDetailsModel(BankAppDataContext context)
        {
            _context = context;
        }

        public Account Account { get; set; }
        public IList<Transaction> Transactions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);

            if (Account == null)
            {
                return NotFound();
            }

            Transactions = await _context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date)
                .Take(20)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnGetLoadMoreAsync(int id, int skip)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == id)
                .OrderByDescending(t => t.Date)
                .Skip(skip)
                .Take(20)
                .ToListAsync();

            return Partial("_TransactionPartial", transactions);
        }
    }
}