using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Services;
using ParkingSystem.Reports;
using QuestPDF.Fluent;
using Microsoft.AspNetCore.Authorization;

namespace ParkingSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportService _service;

        public ReportController(ReportService service)
        {
            _service = service;
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var data = (startDate.HasValue && endDate.HasValue)
                ? _service.GetByDate(startDate.Value, endDate.Value)
                : _service.GetAll();

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(data);
        }
        public IActionResult Print(DateTime? startDate, DateTime? endDate)
        {
            var data = (startDate.HasValue && endDate.HasValue)
                ? _service.GetByDate(startDate.Value, endDate.Value)
                : _service.GetAll();

            var document = new ParkingReportDocument(data);

            var pdf = document.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                "LaporanParkir.pdf");
        }
    }
}