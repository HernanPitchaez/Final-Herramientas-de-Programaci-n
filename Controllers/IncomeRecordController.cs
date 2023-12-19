using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.Services;
using Proyecto_ClubDeportes.ViewModels;

namespace Proyecto_ClubDeportes.Controllers
{
    
    public class IncomeRecordController : Controller
    {
        private readonly IIncomeRecordService _incomeRecordService;
        private readonly IPartnerService _partnerService;

        public IncomeRecordController(IIncomeRecordService incomeRecordService, IPartnerService partnerService)
        {
            _incomeRecordService = incomeRecordService;
            _partnerService = partnerService;
        }
        
        // GET: IncomeRecord/Index
        [Authorize(Roles = "Secretaria, Administrador")]
        public async Task<IActionResult> Index(string filter)
        {
            var partnersDeb = await _incomeRecordService.GetAllDebtors(filter);
            var memberShipFee = await _incomeRecordService.GetMembershipFee();

            var partnersVMDeb = partnersDeb.Select(p => new IncomeRecordViewModel
            {
                Id = p.Id,
                PartnerName = p.PartnerName,
                MembershipFee = p.MembershipFee,
                Sports = p.Sports,
                TotalPrice = p.TotalPrice 
            }).ToList();

            ViewData["Filter"] = filter;
            ViewData["MembershipFee"] = memberShipFee;

            return View(partnersVMDeb); 
        }

        // GET: IncomeRecord/GetAllRecordsPayments
        [Authorize(Roles = "Secretaria, Administrador")]
        public async Task<IActionResult> GetAllRecordsPayments(string filter)
        {
            var recordsPayment = await _incomeRecordService.GetAllPayments(filter);
            var memberShipFee = await _incomeRecordService.GetMembershipFee();

            var paymentsVM = recordsPayment.Select(p => new IncomeRecordViewModel
            {
                Id = p.Id,
                PartnerName = p.PartnerName,
                MembershipFee = p.MembershipFee,
                Sports = p.Sports,
                TotalPrice = p.TotalPrice
            }).ToList();

            ViewData["Filter"] = filter;
            ViewData["MembershipFee"] = memberShipFee;

            return View(paymentsVM);
        }
        
        // GET: IncomeRecord/RecordPayment
        [Authorize(Roles = "Secretaria, Administrador")]
        public async Task<IActionResult> RecordPayment(int? id)
        {
            var partner = await _partnerService.GetById(id.Value);
            var sports = await _incomeRecordService.GetSports(partner.Id);
            
            var membershipFee = await _incomeRecordService.GetMembershipFee();

            var sportsTotalPrice = sports.Sum(s => s.Price);
            var totalPrice = sportsTotalPrice + membershipFee;

            var payment = new IncomeRecordCreateVM
            {
                PartnerId = partner.Id,
                PartnerName = partner.Name,
                MembershipFee = membershipFee,
                TotalPrice = totalPrice,
                SportViewModels = sports.Select(s => new SportViewModel
                {
                    Id = s.Id,
                    Activity = s.Activity,
                    Price = s.Price
                }).ToList()
            };

            ViewData["Sports"] = payment.SportViewModels;

    return View(payment);
        }

        // POST: IncomeRecord/RecordPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretaria, Administrador")]
        public async Task<IActionResult> RecordPayment(int id, IncomeRecordCreateVM partnerPayment)
        {
            if (ModelState.IsValid)
            {
                var newIncomeRecord = new IncomeRecord
                {
                    Id = partnerPayment.Id,
                    Date = partnerPayment.Date,
                    ReceiptNumber = partnerPayment.ReceiptNumber,
                    PaymentType = partnerPayment.PaymentType,
                    MembershipFee = partnerPayment.MembershipFee,
                    TotalPrice = partnerPayment.TotalPrice,
                    PartnerId = partnerPayment.PartnerId
                };

                await _incomeRecordService.AddRecord(newIncomeRecord);

                return RedirectToAction(nameof(Index));
            }

            return View(partnerPayment);
        }
        
        // GET: IncomeRecord/EditMembership
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditMembership()
        {
            var membershipValue = await _incomeRecordService.GetMembershipFee();
            var membership = new Membership { membershipType = "BaseMembership", Value = membershipValue };
            return View(membership);
        }

        // POST: IncomeRecord/EditMembership
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EditMembership(Membership membership)
        {
            if (ModelState.IsValid)
            {
                await _incomeRecordService.SetMembershipFee( membership.Value);
                return RedirectToAction(nameof(Index));
            }
            return View(membership);
        }
    }
}
