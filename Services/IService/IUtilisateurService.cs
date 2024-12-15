using DAL.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IUtilisateurService
    {
        Task<RoleDto?> GetRoleForUser(UtilisateurDto utilisateur);
        Task<UserModel?> LoginAsync(LoginRequest request);
        Task<RegisterModel?> RegisterAsync(RegisterRequest request);
        Task RevokeTokenAsync(string token);
    }
}
