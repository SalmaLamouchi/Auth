using AutoMapper;
using DAL.Entities;
using Services.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public partial class ProfilDto : IMapFrom<Profil>
    {
        public int IdProfil { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public int? Telephone { get; set; }

        public string Adresse { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Profil, ProfilDto>().ReverseMap();

        }
    }
}
