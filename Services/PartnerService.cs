using Microsoft.EntityFrameworkCore;
using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.Services;

public class PartnerService : IPartnerService
{
    private readonly ClubContext _context;

    public PartnerService(ClubContext context)
    {
        _context = context;
    }

    public async Task<List<Partner>> GetAll(string filter)
    {
        var query = from partner in _context.Partner select partner;
            query = _context.Partner.Include(p => p.Sports);

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()) || 
                x.Sports.Any(s => s.Activity.ToLower().Contains(filter.ToLower())));  
            }  

            return await query.ToListAsync();
    }

    public async Task Create(Partner partner)
    {
        _context.Add(partner);
       await _context.SaveChangesAsync();
    }

    public async Task Update(Partner partner)
    {
        _context.Update(partner);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var partner = await _context.Partner.FindAsync(id);
        if (partner != null)
        {
            _context.Partner.Remove(partner);
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task<Partner?> GetById(int? id)
    {
        if (id == null || _context.Partner == null)
        {
            return null;
        }

        return await _context.Partner.Include(x => x.Sports).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Sport>> GetSports()
    {
        return await _context.Sport.ToListAsync();
    }
}