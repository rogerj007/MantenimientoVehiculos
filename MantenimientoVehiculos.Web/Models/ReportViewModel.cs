using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Resources;

namespace MantenimientoVehiculos.Web.Models
{
    public class ReportViewModel
    {
        public string Plaque { get; set; }

        [Display(Name = "Component", ResourceType = typeof(Language))]
        public string ComponentName { get; set; }

        [DisplayName("Km  Hr")]
        public long KmHrMaintenance { get; set; }
        
        [DisplayName("Date Maintenance")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
     
    }
}
