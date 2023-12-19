using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Models;
using Proyecto_ClubDeportes.Utils;

namespace Proyecto_ClubDeportes.ViewModels;

public class PartnerCreateViewModel
{
    public int Id { get; set; }

    [Display(Name="Nombre")]
    public string Name { get; set; }

    [Display(Name="Años")]
    public int Years { get; set; }

    [Display(Name="Sexo")]
    public Gender Gender { get; set; }

    [Display(Name="Nº Telefono")]
    public int NumberPhone { get; set; }

    [Display(Name="Email")]
    public string Email { get; set; } 

    [Display(Name="Dirección")]
    public string Address { get; set; }

    public List<int> SportIds { get; set; }

    public virtual ICollection<Sport>? Sports { get; set; }

}