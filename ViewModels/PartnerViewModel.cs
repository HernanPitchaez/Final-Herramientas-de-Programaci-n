using Proyecto_ClubDeportes.Models;

namespace Proyecto_ClubDeportes.ViewModels;

public class PartnerViewModel
{
    public List<Partner> Partners { get; set;} = new List<Partner>{};

    public string? Filter { get; set;}

}