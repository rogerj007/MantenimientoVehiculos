using MantenimientoVehiculos.Web.Models;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}