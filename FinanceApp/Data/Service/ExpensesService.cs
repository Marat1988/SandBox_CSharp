using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Service
{
    public class ExpensesService : IExtensesService
    {
        private readonly FinanceAppContext _appContext;

        public ExpensesService(FinanceAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task Add(Expense expense)
        {
            _appContext.Expenses.Add(expense);
            await _appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            var expenses = await _appContext.Expenses.ToListAsync();
            return expenses;
        }

        public IQueryable GetChartData()
        {
            var data = _appContext.Expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount)
                });
            return data;
        }
    }
}
