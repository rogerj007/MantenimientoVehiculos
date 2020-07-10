using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MantenimientoVehiculos.Web.Resources;
using Microsoft.Extensions.Localization;

namespace MantenimientoVehiculos.Web.Data.Entities.Base
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        object IEntity.Id { get; } //  set; }

        [Column(TypeName = "nvarchar(150)")]
        [StringLength(150, MinimumLength = 4,ErrorMessage = "The {0} field must have {1} characters.")]
       
        public abstract string Name { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Language), Name = nameof(Language.CreatedDate))]
        public DateTime CreatedDate { get; set; }

        public DateTime CreatedDateLocal => CreatedDate.ToLocalTime();

        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Language), Name = nameof(Language.ModifiedDate))]
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ModifiedDateLocal => ModifiedDate?.ToLocalTime();

        public bool IsEnable { get; set; }

        [Display(ResourceType = typeof(Language), Name = nameof(Language.UserCreated))]
        public UserEntity CreatedBy { get; set; }

        [Display(ResourceType = typeof(Language), Name = nameof(Language.UserModified))]
        public UserEntity ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is BaseEntity<T> entity) && (entity.GetType() == GetType()) && (entity.Id.Equals(Id));
        }
        



    }
}
