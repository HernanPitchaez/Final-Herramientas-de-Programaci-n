using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.Services;

public interface ISportService
{
    Task<List<Sport>> GetAll(string filter);
    Task Create (Sport sport);
    Task Update (Sport sport);
    Task Delete (int id);
    Task<Sport?> GetById(int? id);
}