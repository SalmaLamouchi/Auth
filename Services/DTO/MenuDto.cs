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
    public partial class MenuDto :IMapFrom<Menu>
    {
        
            public int IdMenu { get; set; }

            public string Titre { get; set; }

            public string Description { get; set; }

            public string Link { get; set; }

            public int? ParentId { get; set; }

            public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

            public virtual ICollection<ListRole> ListRoles { get; set; } = new List<ListRole>();

            public virtual Menu? Parent { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Menu, MenuDto>().ReverseMap();

        }
    }
}
    