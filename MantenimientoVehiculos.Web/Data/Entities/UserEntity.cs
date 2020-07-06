using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Enums;
using Microsoft.AspNetCore.Identity;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class UserEntity :IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
        public bool Enable { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDateLocal => CreatedDate.ToLocalTime();
        [DataType(DataType.DateTime)]
        [Display(Name = "Modification Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ModifiedDateLocal => ModifiedDate?.ToLocalTime();

       
        [Display(Name = "User Function")]
        [ForeignKey("UserFunctionId")]
        public  virtual UserFunctionEntity UserFunction { get; set; }
        
    }
}
