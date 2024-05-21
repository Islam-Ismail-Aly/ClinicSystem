using ClinicSystem.Core.Entities;
using ClinicSystem.Web.Models;
using ClinicSystem.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardService _dashboardService;

        public HomeController(ILogger<HomeController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index(bool orderAppointmentsAscending = true)
        {
            var dashboardData = await _dashboardService.GetDashboardDataAsync(orderAppointmentsAscending);

            var model = new DashboardViewModel
            {
                DoctorsCount = dashboardData.DoctorsCount,
                PatientsCount = dashboardData.PatientsCount,
                UsersCount = dashboardData.UsersCount,
                SecretariesCount = dashboardData.SecretariesCount,
                LatestAppointments = dashboardData.LatestAppointments
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
