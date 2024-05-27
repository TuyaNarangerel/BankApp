using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.ViewModels;

namespace MyBankApp.Pages
{
    public class TransferModel : PageModel
    {
        private readonly TransactionService _transactionService;

        public TransferModel(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [BindProperty]
        public int FromAccountId { get; set; }

        [BindProperty]
        public int ToAccountId { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        public string Message { get; set; }

        public List<TransactionViewModel> RecentTransactions { get; set; }

        public async Task OnGetAsync()
        {
            RecentTransactions = await _transactionService.GetRecentTransactions(FromAccountId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Amount <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please provide a valid amount.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var model = new TransferViewModel
            {
                FromAccountId = FromAccountId,
                ToAccountId = ToAccountId,
                Amount = Amount
            };

            bool transferSuccessful = await _transactionService.PerformTransfer(model);
            if (transferSuccessful)
            {
                Message = "Transfer successful.";
                ModelState.Clear();
                FromAccountId = 0;
                ToAccountId = 0;
                Amount = 0;

                RecentTransactions = await _transactionService.GetRecentTransactions(model.FromAccountId);
            }
            else
            {
                Message = "Transfer failed. Please try again.";
            }

            return Page();
        }
    }
}
