namespace Shared.Repository
{
	public interface IRepositoryBase<T>
	{
		Task<List<T>> FindAll();
		Task<T?> FindById(int id);
		Task Create(T entity);
		Task Update(T entity);
		Task Delete(int entity);
	}
}
