using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User_API.Data;

namespace Shared.Repository
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{

		protected readonly ApiContext context;

		public RepositoryBase(ApiContext context) 
		{
			this.context = context;
		}

		public async Task<T?> FindById(int id)
		{
			var entity = await context.Set<T>().FindAsync(id);

			return entity;
		}

		public async Task<List<T>> FindAll()
		{
			var entities = await context.Set<T>().ToListAsync();

			return entities;
		}
		public async Task Create(T entity)
		{
			context.Set<T>().Add(entity);
			await context.SaveChangesAsync();
		}

		public async Task Update(T entity)
		{
			context.Set<T>().Update(entity);
			await context.SaveChangesAsync();
		}

		public async Task Delete(T entity)
		{
			context.Set<T>().Remove(entity);
			await context.SaveChangesAsync();
		}
	}
}
