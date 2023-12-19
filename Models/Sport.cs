using System.ComponentModel.DataAnnotations;
using Proyecto_ClubDeportes.Controllers;

namespace Proyecto_ClubDeportes.Models;

    public class Sport
    {
        public int Id { get; set; }

        [Display(Name="Deporte")]
        public string Activity { get; set; }

        [Display(Name="Precio")]
        public decimal Price { get; set; }

        [Display(Name="Descripción")]
        public string Description { get; set; }

        public virtual ICollection<Partner>? Partners { get; set; }

        public virtual ICollection<IncomeRecord>? IncomeRecords { get; set; }
 
    }