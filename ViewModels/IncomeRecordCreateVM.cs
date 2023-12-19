using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.Utils;

namespace Proyecto_ClubDeportes.ViewModels;

public class IncomeRecordCreateVM
{
    public int Id { get; set; }

     [Display(Name="Fecha")]
    public DateTime Date { get; set; } = DateTime.Now; 

    [Display(Name="Número de recibo")]
    public int ReceiptNumber { get; set; }

    [Display(Name="Tipo de pago")]
    public PaymentType PaymentType { get; set; }

    [Display(Name="Cuota Social")]
    public decimal MembershipFee { get; set; }

    [Display(Name="Precio total")]
    public decimal TotalPrice { get; set; }

    public int PartnerId { get; set; }

    [Display(Name="Nombre de Socio")]
    public string? PartnerName { get; set; }
    
    public virtual ICollection<SportViewModel>? SportViewModels { get; set; }

}