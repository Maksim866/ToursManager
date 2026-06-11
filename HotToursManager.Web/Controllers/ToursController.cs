using Microsoft.AspNetCore.Mvc;
using HotToursManager.Services.Contracts;
using HotToursManager.Models;

namespace HotToursManager.Web.Controllers
{

    /// <summary>
    /// Контроллер для управления турами
    /// </summary>
    public class ToursController : Controller
    {
        private readonly ITourService tourService;

        /// <summary>
        /// Конструктор контроллера, принимающий сервис для работы с турами
        /// </summary>
        public ToursController(ITourService tourService)
        {
            this.tourService = tourService;
        }

        /// <summary>
        /// GET: /Tours
        /// Отображает список всех туров
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var tours = await tourService.GetAllToursAsync();
            return View("~/Views/Home/Index.cshtml", tours);
        }

        /// <summary>
        /// POST: /Tours/Create
        /// Создаёт новый тур
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Destination,DepartureDate,Nights,CostPerPerson,NumberOfPeople,HasWiFi,Surcharges")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                await tourService.AddTourAsync(tour);
                TempData["SuccessMessage"] = "Тур успешно добавлен!";
                return RedirectToAction(nameof(Index));
            }

            // Если есть ошибки, передаём данные обратно в модалку
            TempData["CreateErrors"] = string.Join("; ",
                ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            TempData["OpenCreateModal"] = true;

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: /Tours/Edit/{id}
        /// Загружает данные тура для редактирования
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await tourService.GetTourByIdAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            //Передаём данные тура в модалку через TempData
            TempData["EditTourId"] = tour.Id;
            TempData["EditTourDestination"] = tour.Destination;
            TempData["EditTourDepartureDate"] = tour.DepartureDate.ToString("yyyy-MM-dd");
            TempData["EditTourNights"] = tour.Nights;
            TempData["EditTourCostPerPerson"] = tour.CostPerPerson.ToString();
            TempData["EditTourNumberOfPeople"] = tour.NumberOfPeople;
            TempData["EditTourHasWiFi"] = tour.HasWiFi;
            TempData["EditTourSurcharges"] = tour.Surcharges.ToString();
            TempData["OpenEditModal"] = true;

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /Tours/Edit/{id}
        /// Обновляет данные тура с указанным id
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tour tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await tourService.UpdateTourAsync(tour);
                TempData["SuccessMessage"] = "Тур успешно обновлен!";
                return RedirectToAction(nameof(Index));
            }

            // Если есть ошибки, передаём данные обратно в модалку
            TempData["EditTourId"] = tour.Id;
            TempData["EditTourDestination"] = tour.Destination;
            TempData["EditTourDepartureDate"] = tour.DepartureDate.ToString("yyyy-MM-dd");
            TempData["EditTourNights"] = tour.Nights;
            TempData["EditTourCostPerPerson"] = tour.CostPerPerson.ToString();
            TempData["EditTourNumberOfPeople"] = tour.NumberOfPeople;
            TempData["EditTourHasWiFi"] = tour.HasWiFi;
            TempData["EditTourSurcharges"] = tour.Surcharges.ToString();
            TempData["OpenEditModal"] = true;

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// POST: /Tours/Delete/{id}
        /// Удаляет тур с указанным id
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await tourService.DeleteTourAsync(id);
            TempData["SuccessMessage"] = "Тур успешно удален!";
            return RedirectToAction(nameof(Index));
        }
    }
}
