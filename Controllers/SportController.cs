
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.Services;

namespace Proyecto_ClubDeportes.Controllers
{
    
    public class SportController : Controller
    {
        private readonly ISportService _sportService;

        public SportController(ISportService sportService)
        {
            _sportService = sportService;
        }

        // GET: Sport
        [Authorize(Roles = "Socio, Secretaria, Administrador")]
        public async Task<IActionResult> Index(string filter)
        {
            var sportList = await _sportService.GetAll(filter);
            var sportListVM = new SportViewModel();

            sportListVM.Sports = sportList;

            return View(sportListVM);
        }
        // GET: Sport/Details/5
        [Authorize(Roles = "Secretaria, Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            var sport = await _sportService.GetById(id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }
        [Authorize(Roles = "Administrador")]
        // GET: Sport/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        // POST: Sport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activity,Price,Description")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                await _sportService.Create(sport);
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }
      
        // GET: Sport/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            var sport = await _sportService.GetById(id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }
       
        // POST: Sport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activity,Price,Description")] Sport sport)
        {
            if (id != sport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _sportService.Update(sport);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_sportService.GetById(sport.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }
 
        // GET: Sport/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            var sport = await _sportService.GetById(id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }
     
        // POST: Sport/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sportService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
