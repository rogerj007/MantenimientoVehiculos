using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }

}