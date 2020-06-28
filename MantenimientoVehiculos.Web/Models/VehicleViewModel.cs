using MantenimientoVehiculos.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MantenimientoVehiculos.Web.Models
{
    public class VehicleViewModel:VehicleEntity
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Vehicle Brand.")]
        public int VehicleBrandId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Vehicle Type.")]
        public int VehicleTypeId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Vehicle Status")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Vehicle Type.")]
        public int VehicleStatusId { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Country.")]
        public int CountryId { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Fuel")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Fuel.")]
        public int FuelId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Color")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Color.")]
        public int ColorId { get; set; }



        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
        public IEnumerable<SelectListItem> VehicleBrands { get; set; }
        public IEnumerable<SelectListItem> VehicleTypes { get; set; }
        public IEnumerable<SelectListItem> VehicleStatu { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Fuels { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }

    }
}
