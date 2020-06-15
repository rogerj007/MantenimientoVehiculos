using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
    }

}
