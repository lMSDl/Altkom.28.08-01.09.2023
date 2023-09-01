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
		protected ASPNETContext DbContext { get; }
		protected IMapper Mapper { get; }

		public CrudService(ASPNETContext dbContext)
		{
			DbContext = dbContext;

			Mapper = ConfigureAutoMapper().CreateMapper();
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
			//wywołanie procedury składowej nie zwracającej wyniku
			//_dbContext.Database.ExecuteSqlInterpolated($"exec myprocedure {}");

			//wywołanie procedury składowej zwracające wynik
			//_dbContext.Set<T>().FromSqlRaw("exec abc");

			var dbEntity = Mapper.Map<Tdb>(entity);
			DbContext.Set<Tdb>().Add(dbEntity);
			await DbContext.SaveChangesAsync();
			entity = Mapper.Map<T>(dbEntity);
			return entity; 
		}

		public async Task DeleteAsync(int id)
		{
			var dbEntity = await DbContext.Set<Tdb>().FindAsync(id);
			DbContext.Remove(dbEntity);
			await DbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> ReadAsync()
		{
			var dbEntities = await DbContext.Set<Tdb>().ToListAsync();
			var entities = Mapper.Map<IEnumerable<T>>(dbEntities);
			return entities;
		}

		public async Task<T?> ReadAsync(int id)
		{
			var dbEntity = await DbContext.Set<Tdb>().FindAsync(id);
			var entity = Mapper.Map<T>(dbEntity);
			return entity;
		}

		public async Task UpdateAsync(int id, T entity)
		{
			entity.Id = id;
			var dbEntity = Mapper.Map<Tdb>(entity);
			DbContext.Set<Tdb>().Update(dbEntity);
			await DbContext.SaveChangesAsync();
		}
	}
}
