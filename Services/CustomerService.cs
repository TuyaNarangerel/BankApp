﻿using System.Threading.Tasks;
using DataAccessLayer.Models;
using Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CustomerService
    {
        private readonly BankAppDataContext _context;

        public CustomerService(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<CustomerDetailsViewModel> GetCustomerDetailsAsync(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null) return null;

            var customerDetails = new CustomerDetailsViewModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Givenname + " " + customer.Surname,
                Address = customer.Streetaddress,
                City = customer.City,
                TotalBalance = customer.Dispositions.Sum(d => d.Account.Balance),
                Accounts = customer.Dispositions.Select(d => new AccountViewModel
                {
                    AccountId = d.AccountId,
                    Balance = d.Account.Balance
                }).ToList()
            };

            return customerDetails;
        }
    }
}