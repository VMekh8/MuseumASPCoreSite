namespace MuseumSite.Core.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllItems();
        Task<T> GetItemByName(string name);
        Task<int> Create(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);

    }
}
