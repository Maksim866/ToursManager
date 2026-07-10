using Microsoft.AspNetCore.Mvc;
using HotToursManager.Services.Contracts;
using HotToursManager.Models;
using HotToursManager.Web.ViewModels;

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
        public async Task<IActionResult> Index(string? message)
        {
            var tours = await tourService.GetAllToursAsync();
            var statistics = await tourService.GetStatisticsAsync();

            var viewModel = new ToursViewModel
            {
                Tours = tours,
                Statistics = statistics,
                Message = message
            };
            return View(nameof(Index), viewModel);
        }

        /// <summary>
        /// GET: /Tours/Create
        /// </summary>
        public IActionResult Create()
        {
            return View();
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
            if (tour.DepartureDate.Date < DateTime.Today)
            {
                ModelState.AddModelError("DepartureDate", "Дата вылета не может быть в прошлом");
            }

            if (ModelState.IsValid)
            {
                await tourService.AddTourAsync(tour);
                return RedirectToAction(nameof(Index), new { message = "Тур успешно добавлен!" });
            }

            return View(tour);
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

            return View(tour);
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

            if (tour.DepartureDate.Date < DateTime.Today)
            {
                ModelState.AddModelError("DepartureDate", "Дата вылета не может быть в прошлом");
            }

            if (ModelState.IsValid)
            {
                await tourService.UpdateTourAsync(tour);
                return RedirectToAction(nameof(Index), new { message = "Тур успешно обновлен!" });
            }

            return View(tour);
        }

        /// <summary>
        /// GET: /Tours/Delete/{id}
        /// Отображает страницу подтверждения удаления тура
        /// </summary>
        public async Task<IActionResult> Delete(int id)
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

            return View(tour);
        }

        /// <summary>
        /// POST: /Tours/DeleteConfirmed/{id}
        /// Удаляет тур с указанным id
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await tourService.DeleteTourAsync(id);
            return RedirectToAction(nameof(Index), new { message = "Тур успешно удален!" });
        }
    }
}
