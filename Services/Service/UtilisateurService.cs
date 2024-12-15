using AutoMapper;
using DAL.Entities;
using DAL.IRepository;
using DAL.Models;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using Services.DTO;
using Services.IService;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UtilisateurService : ServiceAsync<Utilisateur, UtilisateurDto>, IUtilisateurService
    {

        private readonly IServiceAsync<Utilisateur, UtilisateurDto> srvUtilisateur;
        private readonly ITokenService srvToken;
        private readonly IRepositoryAsync<Utilisateur> UtilisateurRepository;
        private readonly IMapper mapper;
        public UtilisateurService(IRepositoryAsync<Utilisateur> _UtilisateurRepository,
                            IServiceAsync<Utilisateur, UtilisateurDto> _srvUtilisateur,
                            ITokenService _srvToken,
                            IMapper _mapper)
            : base(_UtilisateurRepository, _mapper)
        {
            srvUtilisateur = _srvUtilisateur;
            UtilisateurRepository = _UtilisateurRepository;
            srvToken = _srvToken;
            mapper = _mapper;

        }

        public async Task<RoleDto?> GetRoleForUser(UtilisateurDto utilisateur)
        {
            var user = await srvUtilisateur.GetFirstOrDefault(
                predicate: u => u.Email == utilisateur.Email, // Recherche par email (ou un autre champ)
                include: u => u.Include(r => r.IdRoleNavigation), // Inclure l'entité Role
                disableTracking: true
            );

            if (user != null && user.IdRoleNavigation != null)
            {
                // Mapper l'entité Role vers RoleDto
                var roleDto = mapper.Map<RoleDto>(user.IdRoleNavigation);
                return roleDto;
            }

            return null; // Retourner null si l'utilisateur ou le rôle n'est pas trouvé
        }


        public async Task<UserModel?> LoginAsync(LoginRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                throw new ArgumentException("Email and password must be provided.");
            }

            var usr = await srvUtilisateur.GetFirstOrDefault(
                          predicate: (i => i.Email == login.Email),
                          include: (u => u.Include(s => s.IdRoleNavigation)),
                          disableTracking: true
                      );

            if (usr == null || !BCrypt.Net.BCrypt.Verify(login.Password, usr.Motdepasse))
            {
                return null; // Utilisateur non trouvé ou mot de passe incorrect
            }

            var Tkn = srvToken.GenerateToken(usr);

            return new UserModel
            {
                IdUser = usr.IdUser,
                Email = usr.Email,
                IdRole = usr.IdRole,
                Token = Tkn
            };
        }

        private bool IsPasswordValid(string password)
        {
            if (password.Length < 8) return false; // Longueur minimale
            if (!password.Any(char.IsUpper)) return false; // Au moins une majuscule
            if (!password.Any(char.IsLower)) return false; // Au moins une minuscule
            if (!password.Any(char.IsDigit)) return false; // Au moins un chiffre
            if (!password.Any(ch => "!@#$%^&*()_+".Contains(ch))) return false; // Caractère spécial
            return true;
        }


        public async Task<RegisterModel?> RegisterAsync(RegisterRequest request)
        {
            // Vérification des champs requis
            if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
            {
                throw new ArgumentException("Password fields are required.");
            }
            if (!IsPasswordValid(request.Password))
            {
                throw new ArgumentException("Password does not meet the security requirements.");
            }
            if (request.Password != request.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email is required.");
            }

            // Vérifiez si l'email existe déjà
            var existingUser = await srvUtilisateur.GetFirstOrDefault(u => u.Email == request.Email, disableTracking: true);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            // Hachage du mot de passe
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Création de l'utilisateur
            var newUser = new Utilisateur
            {
                Email = request.Email,
                Motdepasse = hashedPassword,
                Matricule = " ", // Matricule peut être vide ou généré
                //IdRole= 17
               
            };

            // Enregistrement en base de données
            await UtilisateurRepository.Add(newUser);
            await UtilisateurRepository.Save();

            return new RegisterModel
            {
                Email = newUser.Email,
                Password=newUser.Motdepasse,
                ConfirmPassword=request.ConfirmPassword
                
            };
        }


        // Method to revoke the token
        public void RevokeToken(string token)
        {
            srvToken.RevokeToken(token); // Add token to revoked list
        }

        // Check if the token has been revoked
        public bool IsTokenRevoked(string token)
        {
            return srvToken.IsTokenRevoked(token);
        }

        // Placeholder methods (could be implemented later)
        public Task<UtilisateurDto?> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task RevokeTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

    }
}
