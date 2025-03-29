using FinanceApp.Data;
using FinanceApp.Data.Service;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExtensesService _extensesService;

        public ExpenseController(IExtensesService extensesService)
        {
            _extensesService = extensesService;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _extensesService.GetAll();
            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _extensesService.Add(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public IActionResult GetChart()
        {
            var data = _extensesService.GetChartData();
            return Json(data);
        }

    }
}
