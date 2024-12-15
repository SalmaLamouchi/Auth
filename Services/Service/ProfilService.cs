//using AutoMapper;
//using DAL.Entities;
//using DAL.IRepository;
//using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol.Core.Types;
//using Services.DTO;
//using Services.IService;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Service
//{
//    public class  ProfilService: ServiceAsync<Profil, ProfilDto>, IProfilService
//    {
//        private readonly IRepositoryAsync<Profil> profilRepository;
//        private readonly IMapper mapper;
//        private readonly IServiceAsync<Profil, ProfilDto> srvProfil;


//        public ProfilService(IRepositoryAsync<Profil> _profilRepository, IMapper _mapper,IServiceAsync<Profil,ProfilDto> _srvProfil)
//            : base(_profilRepository, _mapper)
//        {
//            profilRepository = _profilRepository;
//            mapper = _mapper;
//            srvProfil = _srvProfil;
//        }
//        public async Task<ProfilDto> GetProfilWithRoleIdsAsync(int idProfil)
//        {
//            // Inclure la navigation vers les rôles
//            var result = await profilRepository.GetFirstOrDefault(
//                predicate: p => ((Profil)(object)p).IdProfil == idProfil,
//                include: query => query.Include(p => ((Profil)(object)p).Roles)
//            );

//            if (result == null)
//                throw new Exception("Profil introuvable.");

//            // Mapper les données en DTO
//            var profil = mapper.Map<ProfilDto>(result);

//            // Ajouter les IdRole à partir des rôles
//            profil.Roles = ((Profil)(object)result).Roles.Select(r => r.IdRole).ToList();

//            return profil;
//        }



//    }

//}