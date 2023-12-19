using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.Services;

public interface IPartnerService
{
    Task<List<Partner>> GetAll(string filter);
    Task Create (Partner partner);
    Task Update (Partner partner);
    Task Delete (int id);
    Task<Partner?> GetById(int? id);
    Task<List<Sport>> GetSports();
}
