using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.ViewModels;

public class IncomeRecordViewModel
{
    public int Id { get; set; }

    [Display(Name="Deportes")]
    public ICollection<Sport>? Sports { get; set; }

    
    public List<Partner> Partners { get; set;} = new List<Partner>{};

    public List<Partner> RecordsPayments { get; set; } = new List<Partner>{};

    public List<PartnerRecordVM> PartnerRecordVMs { get; set; } = new List<PartnerRecordVM>{};
    
    [Display(Name="Cuota Social")]
    public decimal MembershipFee { get; set; }

    [Display(Name="Precio total")]
    public decimal TotalPrice { get; set; }

    public string? Filter { get; set;}

    public string? MembershipType { get; set; }

    public decimal MembershipValue { get; set; }

    [Display(Name="Nombre de Socio")]
    public string? PartnerName { get; set; }

}