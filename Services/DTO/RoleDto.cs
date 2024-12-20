﻿using AutoMapper;
using DAL.Entities;
using Services.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class RoleDto : IMapFrom<Role>
    {
        public int IdRole { get; set; }

        public int IdProfil { get; set; }

        public string TypeRole { get; set; }

        public virtual Profil? IdProfilNavigation { get; set; }

        public virtual ICollection<ListRole> ListRoles { get; set; } = new List<ListRole>();

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDto>().ReverseMap();

        }
    }
}
