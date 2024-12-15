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
    public class ListRoleDto : IMapFrom<ListRole>
    {
        public int IdRole { get; set; }

        public int IdMenu { get; set; }

        public bool? Etat { get; set; }

        public virtual Menu? IdMenuNavigation { get; set; }

        public virtual Role? IdRoleNavigation { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListRole, ListRoleDto>().ReverseMap();

        }
    }
}
