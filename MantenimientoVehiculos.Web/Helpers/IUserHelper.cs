﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MantenimientoVehiculos.Web.Data.Entities;
using MantenimientoVehiculos.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace MantenimientoVehiculos.Web.Helpers
{
    public interface IUserHelper
    {
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<UserEntity> GetUserAsync(string email);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<List<UserEntity>> GetAllUserAsync();

    }

}
