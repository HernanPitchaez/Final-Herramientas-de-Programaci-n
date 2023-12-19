using Microsoft.EntityFrameworkCore;
using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.ViewModels;

namespace Proyecto_ClubDeportes.Services;

public class IncomeRecordService : IIncomeRecordService
{
    private readonly ClubContext _context;

    public IncomeRecordService(ClubContext context)
    {
        _context = context;
    }
    public async Task<List<PartnerRecordVM>> GetAllDebtors(string filter)
    {
         var membershipFee = await _context.Membership
            .OrderBy(m => m.Id)
            .Select(m => m.Value)
            .FirstOrDefaultAsync();

         var query = _context.Partner
            .Include(p => p.IncomeRecords)
            .Include(p => p.Sports)
            .Where(p => !p.IncomeRecords.Any());
            
        if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()) 
                || x.Sports.Any(s => s.Activity.ToLower().Contains(filter.ToLower())));
            }  

        var partnerList = await query.ToListAsync(); 

        var partnerRecords = partnerList.Select(partner => new PartnerRecordVM
            {
                Id = partner.Id,
                PartnerName = partner.Name,
                Sports = partner.Sports,
                MembershipFee = membershipFee,
                TotalPrice = partner.Sports.Sum(s => s.Price) + membershipFee
            }).ToList();   

        return partnerRecords;
    }
    
    public async Task<List<PartnerRecordVM>> GetAllPayments(string filter)
    {
            var query = await _context.Partner
            .Include(p => p.IncomeRecords)
            .Include(p => p.Sports)
            .Where(p => p.IncomeRecords.Any())
            .ToListAsync();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()) 
            || x.Sports.Any(s => s.Activity.ToLower().Contains(filter.ToLower()))).ToList();
        }   
        
        var partnersPay = query.Select(p => new PartnerRecordVM
            {
                Id = p.Id,
                PartnerName = p.Name,
                Sports = p.Sports,
                MembershipFee = p.IncomeRecords.FirstOrDefault()?.MembershipFee ?? 0m,
                TotalPrice = p.Sports.Sum(s => s.Price) + (p.IncomeRecords.FirstOrDefault()?.MembershipFee ?? 0m)
            }).ToList();   

        return partnersPay;
    }

    public async Task AddRecord(IncomeRecord incomeRecord)
    {
        _context.Add(incomeRecord);
       await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetMembershipFee()
    {
        var setting = await _context.Membership.SingleOrDefaultAsync(m => m.membershipType == "BaseMembership");
        return setting != null ? setting.Value : 2500;
    }

    public async Task SetMembershipFee(decimal newMembershipFee)
    {
        var setting = await _context.Membership.SingleOrDefaultAsync(m => m.membershipType == "BaseMembership");
        if (setting != null)
        {
            setting.Value = newMembershipFee;
        }
        else
        {
            setting = new Membership { membershipType = "BaseMembership", Value = newMembershipFee };
            _context.Membership.Add(setting);
        }
        await _context.SaveChangesAsync();
    }

    public async Task Update(IncomeRecord incomeRecord)
    {
        _context.Update(incomeRecord);
        await _context.SaveChangesAsync();
    }
    
    public async Task Delete(int id)
    {
        var partnerRecord = await _context.IncomeRecord.FindAsync(id);
        if (partnerRecord != null)
        {
            _context.IncomeRecord.Remove(partnerRecord);
        }
        
        await _context.SaveChangesAsync();
    }
    
    public async Task<Partner?> GetById(int? id)
    {
        if (id == null || _context.Partner == null)
        {
            return null;
        }

        return await _context.Partner
            .Include(p => p.IncomeRecords)
            .Where(p => !p.IncomeRecords.Any())
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<List<Sport>> GetSports(int id)
    {
         var partner = await _context.Partner
                .Include(p => p.Sports)
                .FirstOrDefaultAsync(p => p.Id == id);
          
          return (List<Sport>)partner.Sports;

    }
}
