using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbFirst.Services
{
	public class CrudService<T, Tdb> : ICrudService<T> where T : Entity where Tdb : class
	{
		private readonly ASPNETContext _dbContext;
		private readonly IMapper _mapper;

		public CrudService(ASPNETContext dbContext)
		{
			_dbContext = dbContext;

			_mapper = ConfigureAutoMapper().CreateMapper();
		}

		protected virtual MapperConfiguration ConfigureAutoMapper()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<T, Tdb>();
				cfg.CreateMap<Tdb, T>();
			});
		}


		public async Task<T> CreateAsync(T entity)
		{
			var dbEntity = _mapper.Map<Tdb>(entity);
			_dbContext.Set<Tdb>().Add(dbEntity);
			await _dbContext.SaveChangesAsync();
			entity = _mapper.Map<T>(dbEntity);
			return entity; 
		}

		public async Task DeleteAsync(int id)
		{
			var dbEntity = await _dbContext.Set<Tdb>().FindAsync(id);
			_dbContext.Remove(dbEntity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> ReadAsync()
		{
			var dbEntities = await _dbContext.Set<Tdb>().ToListAsync();
			var entities = _mapper.Map<IEnumerable<T>>(dbEntities);
			return entities;
		}

		public async Task<T?> ReadAsync(int id)
		{
			var dbEntity = await _dbContext.Set<Tdb>().FindAsync(id);
			var entity = _mapper.Map<T>(dbEntity);
			return entity;
		}

		public async Task UpdateAsync(int id, T entity)
		{
			entity.Id = id;
			var dbEntity = _mapper.Map<Tdb>(entity);
			_dbContext.Set<Tdb>().Update(dbEntity);
			await _dbContext.SaveChangesAsync();
		}
	}
}
