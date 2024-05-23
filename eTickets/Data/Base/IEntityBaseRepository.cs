namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}
