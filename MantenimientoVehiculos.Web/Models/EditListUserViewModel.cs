using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Models
{
    public class EditListUserViewModel: EditUserViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Register as")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a role.")]
        public int UserTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Register as")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a User Funcion.")]
        public int? UserFuncionId { get; set; }

        public bool Enable { get; set; } 

        public IEnumerable<SelectListItem> UserTypes { get; set; }

        public IEnumerable<SelectListItem> UserFuncion { get; set; }

    }
}
