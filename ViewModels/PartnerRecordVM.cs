using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.ViewModels;

public class PartnerRecordVM
{
    public int Id { get; set; }
    public string? PartnerName { get; set; }
    public ICollection<Sport>? Sports { get; set; }
    public decimal MembershipFee { get; set; }
    public decimal TotalPrice { get; set; }
}