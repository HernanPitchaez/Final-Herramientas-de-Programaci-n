using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.Services;
using Proyecto_ClubDeportes.ViewModels;

namespace Proyecto_ClubDeportes.Controllers
{
    [Authorize(Roles = "Secretaria, Administrador")]
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        // GET: Partner
        public async Task<IActionResult> Index(string filter)
        {
            var partnerList = await _partnerService.GetAll(filter);
            var partnerListVM = new PartnerViewModel();

            partnerListVM.Partners = partnerList;

            return View(partnerListVM); 
        }

        // GET: Partner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var partner = await _partnerService.GetById(id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Partner/Create
        public async Task<IActionResult> Create()
        {
            var sportList = await _partnerService.GetSports();
            
            var sportVM = sportList.Select(s => new SportViewModel
            {
                Id = s.Id,
                Activity = s.Activity,
                IsSelected = false
            }).ToList();

            ViewData["Sports"] = sportVM;
            
            return View();
        }

        // POST: Partner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Years,Gender,NumberPhone,Email,Address,SportIds")] PartnerCreateViewModel partner)
        {
            if (ModelState.IsValid)
            {
                 var sportList = await _partnerService.GetSports();
                 var sportFilteredList = sportList.Where(x => partner.SportIds.Contains(x.Id)).ToList();
                 
                 var newPartner = new Partner {
                    Name = partner.Name,
                    Years = partner.Years,
                    Gender = partner.Gender,
                    NumberPhone = partner.NumberPhone,
                    Email = partner.Email,
                    Address = partner.Address,
                    Sports = sportFilteredList
                 } ;

                await _partnerService.Create(newPartner);
                return RedirectToAction(nameof(Index));
            }
            return View(partner);
        }

        // GET: Partner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var partner = await _partnerService.GetById(id);

            if (partner == null)
            {
                return NotFound();
            }
            
            var editPartner = new PartnerCreateViewModel {
            Name = partner.Name,
            Years = partner.Years,
            Gender = partner.Gender,
            NumberPhone = partner.NumberPhone,
            Email = partner.Email,
            Address = partner.Address,
            Sports = partner.Sports
            } ;
            
            var sports = await _partnerService.GetSports();
            var sportsVM = sports.Select(s => new SportViewModel
            {
                Id = s.Id,
                Activity = s.Activity,
                IsSelected = partner.Sports.Any(ps => ps.Id == s.Id)
            }).ToList();

            ViewData["Sports"] = sportsVM;
            
            return View(editPartner);
        }

        // POST: Partner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Years,Gender,NumberPhone,Email,Address,SportIds")] PartnerCreateViewModel partner)
        {
            if (id != partner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { 
                try
                {
                    var editPartner = await _partnerService.GetById(id);

                    editPartner.Name = partner.Name;
                    editPartner.Years = partner.Years;
                    editPartner.Gender = partner.Gender;
                    editPartner.NumberPhone = partner.NumberPhone;
                    editPartner.Email = partner.Email;
                    editPartner.Address = partner.Address;

                    // Update the sports
                    var sports = await _partnerService.GetSports();

                    // Get the current sport ids
                    var currentSportIds = editPartner.Sports.Select(s => s.Id).ToList();

                    // Find the sports to add and to remove
                    var sportsToAdd = sports.Where(s => partner.SportIds.Contains(s.Id) && !currentSportIds.Contains(s.Id)).ToList();
                    var sportsToRemove = editPartner.Sports.Where(s => !partner.SportIds.Contains(s.Id)).ToList();

                    // Add new sports
                    foreach (var sport in sportsToAdd)
                    {
                        editPartner.Sports.Add(sport);
                    }

                    // Remove sports
                    foreach (var sport in sportsToRemove)
                    {
                        editPartner.Sports.Remove(sport);
                    }
                 
                    await _partnerService.Update(editPartner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_partnerService.GetById(partner.Id) == null)
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
            return View(partner);
        }

        // GET: Partner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var partner = await _partnerService.GetById(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _partnerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
