using Microsoft.EntityFrameworkCore;
using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.Services;

public class SportService : ISportService
{
    private readonly ClubContext _context;

    public SportService (ClubContext context)
    {
        _context = context;
    }

    public async Task<List<Sport>> GetAll(string filter)
    {
        var query = from sport in _context.Sport select sport;
        query = _context.Sport.Include(p => p.Partners);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.Activity.ToLower().Contains(filter.ToLower()) || 
            x.Description.ToLower().Contains(filter.ToLower()));  
        }  

        return await query.ToListAsync();
    }

    public async Task Create(Sport sport)
    {
        _context.Add(sport);
       await _context.SaveChangesAsync();
    }

    public async Task Update(Sport sport)
    {
        _context.Update(sport);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var sport = await _context.Sport.FindAsync(id);
        if (sport != null)
        {
            _context.Sport.Remove(sport);
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task<Sport?> GetById(int? id)
    {
        if (id == null || _context.Sport == null)
        {
            return null;
        }

        return await _context.Sport.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Partner>> GetSports()
    {
        return await _context.Partner.ToListAsync();
    }
}