using AutoMapper;
//using AutoMapper.Extensions.ExpressionMapping;

//using Core.Specifications;

using DAL.IRepository;


using Microsoft.EntityFrameworkCore.Query;

using Services.IService;

using System.Linq.Expressions;


namespace Services.Service
{
    public class ServiceAsync<TEntity, TDto> : IServiceAsync<TEntity, TDto>
        where TDto : class where TEntity : class
    {
        private readonly IRepositoryAsync<TEntity> _profilRepository;
        private readonly IMapper _mapper;




        public ServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper)
        {
            _profilRepository = repository;
            _mapper = mapper;


        }

        #region Add
        public async Task Add(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _profilRepository.Add(entity);
        }

        public async Task Add(IEnumerable<TDto> tDto)
        {
            var entity = _mapper.Map<IEnumerable<TEntity>>(tDto);
            var e = entity.AsEnumerable();
            await _profilRepository.Add(e);
        }

        public async Task AddSansCle(IEnumerable<TDto> tDto)
        {
            var entity = _mapper.Map<IEnumerable<TEntity>>(tDto);
            var e = entity.AsEnumerable();
            await _profilRepository.AddSansCle(e);
        }

        public async Task AddEntities(IEnumerable<TEntity> tentity)
        {
            await _profilRepository.Add(tentity);
        }

        #endregion



        public async Task<int> Count(Expression<Func<TDto, bool>> predicate = null)
        {
            var predicates = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            return await _profilRepository.Count(predicates);
        }

        public async Task Delete(object id)
        {
            await _profilRepository.Delete(id);
        }

        public async Task Delete(TDto entityToDelete)
        {
            var entity = _mapper.Map<TEntity>(entityToDelete);
            await _profilRepository.Delete(entity);
        }

        public async Task Delete(IEnumerable<TDto> entities)
        {
            var entity = _mapper.Map<IEnumerable<TEntity>>(entities);
            var e = entity.AsEnumerable();
            await _profilRepository.Delete(e);
        }


        public async Task<bool> Exists(Expression<Func<TDto, bool>> predicate)
        {
            var predicates = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            return await _profilRepository.Exists(predicates);
        }


        public int ExecuteQuery(string Sql)
        {
            return _profilRepository.ExecuteQuery(Sql);
        }

        public IQueryable<TDto> FromSql(string sql, params object[] parameters)
        {

            var result = _profilRepository.FromSql(sql, parameters).ToList();
            var ret = _mapper.Map<List<TDto>>(result);
            return ret.AsQueryable();
        }



        public IQueryable<TDto> GetAll()
        {
            var result = _profilRepository.GetAll();
            var ret = _mapper.Map<List<TDto>>(result);
            return ret.AsQueryable();
        }



        public async Task<TDto> GetById(params object[] keyValues)
        {
            var result = await _profilRepository.GetById(keyValues);
            return _mapper.Map<TDto>(result);
        }



        public async Task<TDto> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true)
        {

            var result = await _profilRepository.GetFirstOrDefault(predicate, orderBy, include, disableTracking);
            return _mapper.Map<TDto>(result);
        }





        public async Task<IEnumerable<TDto>> GetMuliple(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true)
        {

            var result = await _profilRepository.GetMuliple(predicate, orderBy, include, disableTracking);
            var e = _mapper.Map<List<TDto>>(result);

            return e.AsEnumerable();
        }


        public async Task Save()
        {
            await _profilRepository.Save();
        }

        public async Task Update(TDto entityTDto)
        {
            var ret = _mapper.Map<TEntity>(entityTDto);
            await _profilRepository.Update(ret);
        }

        public async Task Update(IEnumerable<TDto> entities)
        {
            var ret = _mapper.Map<List<TEntity>>(entities);
            var e = ret.AsEnumerable();
            await _profilRepository.Update(e);
        }


    }
}