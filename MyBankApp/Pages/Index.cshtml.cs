using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBankApp.ViewModels;
using Services;

namespace MyBankApp.Pages
{
    //public class IndexModel : PageModel
    //{
    //    private readonly ILogger<IndexModel> _logger;

    //    public IndexModel(ILogger<IndexModel> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public void OnGet()
    //    {

    //    }
    //}

    public class IndexModel : PageModel
    {
        private readonly TransactionService _transactionService;

        public IndexModel(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public Statistics Statistics { get; private set; }

        public async Task OnGetAsync()
        {
            Statistics = new Statistics
            {
                CustomerCount = await _transactionService.GetCustomerCountAsync(),
                AccountCount = await _transactionService.GetAccountCountAsync(),
                TotalBalance = await _transactionService.GetTotalBalanceAsync()
            };
        }
    }
}