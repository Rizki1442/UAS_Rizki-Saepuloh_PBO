using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Models;
using ParkingSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace ParkingSystem.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService _service;

        public VehicleController(VehicleService service)
        {
            _service = service;
        }

        // Menampilkan daftar kendaraan
        public IActionResult Index(string? keyword)
        {
            var data = string.IsNullOrWhiteSpace(keyword)
                ? _service.GetAll()
                : _service.Search(keyword);

            ViewBag.Keyword = keyword;

            return View(data);
        }

        // Form tambah kendaraan
        public IActionResult Create()
        {
            return View();
        }

        // Simpan kendaraan baru
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return View(vehicle);

            _service.Add(vehicle);

            TempData["Success"] = "Data kendaraan berhasil ditambahkan.";

            return RedirectToAction(nameof(Index));
        }

        // Form edit
        public IActionResult Edit(Guid id)
        {
            var vehicle = _service.GetById(id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        // Simpan perubahan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(vehicle);

            _service.Update(vehicle);

            TempData["Success"] = "Data kendaraan berhasil diubah.";

            return RedirectToAction(nameof(Index));
        }

        // Konfirmasi hapus
        public IActionResult Delete(Guid id)
        {
            var vehicle = _service.GetById(id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        // Hapus data
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Data kendaraan berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}