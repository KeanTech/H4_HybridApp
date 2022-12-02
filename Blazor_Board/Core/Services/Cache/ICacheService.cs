using Blazor_Board.Models.Data;

namespace Blazor_Board.Core.Services.Cache
{
    public interface ICacheService<T>
    {
        public Task<List<T>> GetAll();
        public Task<T?> Get(int id);
        public Task<bool> Exist(T cacheType);
        public void Add(T cacheType);
        public void AddAll(List<T> cache);
        public void Update(T cacheType);
        public void UpdateAll(List<T> cache);
        public void Remove(T cacheType);
        public void Clear();
    }
}
