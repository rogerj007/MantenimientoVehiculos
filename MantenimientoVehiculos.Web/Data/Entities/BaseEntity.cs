using System;
using System.ComponentModel.DataAnnotations;

namespace MantenimientoVehiculos.Web.Data.Entities
{
    public abstract class BaseEntity
    {
        protected bool Equals(BaseEntity other)
        {
            return Id == other.Id && CreationDate.Equals(other.CreationDate);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return HashCode.Combine(Id, CreationDate);
        }

        //public int Id { get; set; }
        public virtual int Id { get; set; }
        protected virtual object Actual => this;



        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }
      
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
