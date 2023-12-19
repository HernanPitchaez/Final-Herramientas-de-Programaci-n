using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Utils;

namespace Proyecto_ClubDeportes.Models;

public class IncomeRecord
{
    public int Id { get; set; }

    [Display(Name="Fecha")]
    public DateTime Date { get; set; }

    [Display(Name="Número de recibo")]
    public int ReceiptNumber { get; set; }

    [Display(Name="Tipo de pago")]
    public PaymentType PaymentType { get; set; }

    [Display(Name="Cuota Social")]
    public decimal MembershipFee { get; set; }

    [Display(Name="Precio total")]
    public decimal TotalPrice { get; set; }

    public int PartnerId { get; set; }

    public virtual Partner Partner { get; set; }
    
    public virtual ICollection<Sport> Sports { get; set; }
}