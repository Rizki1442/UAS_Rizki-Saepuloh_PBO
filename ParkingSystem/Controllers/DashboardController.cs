using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Services;

namespace ParkingSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var model = _service.GetDashboard();
            return View(model);
        }
    }
}