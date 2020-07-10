using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Enums;
using MantenimientoVehiculos.Web.Resources;
using Microsoft.AspNetCore.Identity;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public class UserEntity :IdentityUser
    {
        [Display(Name = "Document")]
        [StringLength(20, MinimumLength = 2, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [MaxLength(20)]
        public string Document { get; set; }


        
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [MaxLength(50)]
        public string LastName { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.MaxLength_Message))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Required_Message))]
        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
        [Display(Name = "User Type")]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "Required_Message")]
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
