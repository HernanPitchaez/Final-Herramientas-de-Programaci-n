using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.ViewModels;

namespace Proyecto_ClubDeportes.Services;

public interface IIncomeRecordService
{
    Task<List<PartnerRecordVM>> GetAllDebtors(string filter);
    Task<List<PartnerRecordVM>> GetAllPayments(string filter);
    Task AddRecord (IncomeRecord incomeRecord);
    Task Update (IncomeRecord incomeRecord);
    Task Delete (int id);
    Task<Partner?> GetById(int? id);
    Task<List<Sport>> GetSports(int id);
    Task<decimal> GetMembershipFee();
    Task SetMembershipFee(decimal newMembershipFee);
}
