using System;

namespace MantenimientoVehiculos.Web.Data.Entities.Base
{
    public interface IModifiableEntity
    {
        string Name { get; set; }
    }
  
    public interface IEntity : IModifiableEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        UserEntity CreatedBy { get; set; }
        UserEntity ModifiedBy { get; set; }
        byte[] Version { get; set; }
    }
    
    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}