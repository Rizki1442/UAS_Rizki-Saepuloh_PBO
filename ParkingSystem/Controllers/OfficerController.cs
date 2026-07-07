using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Models;
using ParkingSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace ParkingSystem.Controllers
{
    public class OfficerController : Controller

    {
        private readonly OfficerService _service;

        public OfficerController(OfficerService service)
        {
            _service = service;
        }

        public IActionResult Index(string? keyword)
        {
            var data = string.IsNullOrWhiteSpace(keyword)
                ? _service.GetAll()
                : _service.Search(keyword);

            ViewBag.Keyword = keyword;

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Officer officer)
        {
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        Console.WriteLine($"{item.Key} : {error.ErrorMessage}");
                    }
                }

                return View(officer);
            }

            try
            {
                _service.Add(officer);

                TempData["Success"] = "Data petugas berhasil ditambahkan.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                Console.WriteLine(ex.ToString());

                return View(officer);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var officer = _service.GetById(id);

            if (officer == null)
                return NotFound();

            return View(officer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Officer officer)
        {
            if (id != officer.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(officer);

            _service.Update(officer);

            TempData["Success"] = "Data petugas berhasil diubah.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var officer = _service.GetById(id);

            if (officer == null)
                return NotFound();

            return View(officer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.Delete(id);

            TempData["Success"] = "Data petugas berhasil dihapus.";

            return RedirectToAction(nameof(Index));
        }
    }
}