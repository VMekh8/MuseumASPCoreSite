namespace MuseumSite.Core.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllItems();
        Task<T> GetItemByName(string name);
        Task<Guid> Create(T entity);
        Task<Guid> Update(T entity);
        Task<Guid> Delete(Guid id);

    }
}
