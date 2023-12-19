using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Controllers;

namespace Proyecto_ClubDeportes.Models;

public class SportViewModel
    {
        public int Id { get; set; }

        [Display(Name="Deporte")]
        public string Activity { get; set; }

        [Display(Name="Precio")]
        public decimal Price { get; set; }

        [Display(Name="Descripción")]
        public string Description { get; set; }

        public List<Sport> Sports { get; set;} = new List<Sport>{};

        public string? Filter { get; set;}

        public bool IsSelected { get; set; }

        public virtual ICollection<Partner>? Partners { get; set; }
    }