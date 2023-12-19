using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Controllers;
using Proyecto_ClubDeportes.Utils;

namespace Proyecto_ClubDeportes.Models;
public class Membership
{
    public int Id { get; set; }

    [Display(Name="Tipo de Cuota Social")]
    public string membershipType { get; set; }

    [Display(Name="Precio")]
    public decimal Value { get; set; }    
}