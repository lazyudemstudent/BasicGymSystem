using Microsoft.AspNetCore.Mvc;
using SistemaGimnasio.Logic.Services.Interfaces;
using SistemaGimnasio.Web.Models;
using System.Diagnostics;

namespace SistemaGimnasio.Web.Controllers
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

        public async Task<IActionResult> Index()
        {
            // El inicio es público, pero el resumen administrativo solo se consulta
            // cuando existe una sesión autenticada.
            if (User.Identity?.IsAuthenticated != true)
            {
                return View(model: null);
            }

            var resumen = await _dashboardService.ObtenerResumenAsync();
            return View(resumen);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
