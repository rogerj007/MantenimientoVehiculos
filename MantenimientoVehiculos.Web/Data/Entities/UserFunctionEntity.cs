using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class UserFunctionEntity
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "The {0} field must have {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string UserFunction { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime CreationDateLocal => CreationDate.ToLocalTime();


        [DataType(DataType.DateTime)]
        [Display(Name = "Modification Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime? ModificationDate { get; set; }
        public DateTime? ModificationDateLocal => ModificationDate?.ToLocalTime();
    }
}
