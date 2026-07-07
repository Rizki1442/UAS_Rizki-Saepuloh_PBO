using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkingSystem.Models;
using ParkingSystem.Services;
using QuestPDF.Fluent;
using ParkingSystem.Reports;
using Microsoft.AspNetCore.Authorization;

namespace ParkingSystem.Controllers
{
    public class ParkingController : Controller
    {
        private readonly ParkingService _parkingService;
        private readonly VehicleService _vehicleService;
        private readonly OfficerService _officerService;

        public ParkingController(
            ParkingService parkingService,
            VehicleService vehicleService,
            OfficerService officerService)
        {
            _parkingService = parkingService;
            _vehicleService = vehicleService;
            _officerService = officerService;
        }

        public IActionResult Index()
        {
            var data = _parkingService.GetAll();

            return View(data);
        }

        public IActionResult CheckIn()
        {
            LoadDropdown();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckIn(ParkingTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Vehicles = _vehicleService.GetAll();
                ViewBag.Officers = _officerService.GetAll();
                return View(transaction);
            }

            try
            {
                _parkingService.CheckIn(transaction);

                TempData["Success"] = "Kendaraan berhasil masuk area parkir.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                ViewBag.Vehicles = _vehicleService.GetAll();
                ViewBag.Officers = _officerService.GetAll();

                return View(transaction);
            }
        }

        public IActionResult CheckOut(Guid id)
        {
            try
            {
                _parkingService.CheckOut(id);

                TempData["Success"] = "Kendaraan berhasil keluar.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        private void LoadDropdown()
        {
            ViewBag.VehicleId = new SelectList(
                _vehicleService.GetAll(),
                "Id",
                "PlateNumber");

            ViewBag.OfficerId = new SelectList(
                _officerService.GetAll(),
                "Id",
                "Name");
        }
        public IActionResult Print(Guid id)
        {
            var transaction = _parkingService.GetById(id);

            if (transaction == null)
                return NotFound();

            var document = new ParkingTicketDocument(transaction);

            var pdf = document.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"Ticket-{transaction.Vehicle?.PlateNumber}.pdf");
        }
    }
}