using Blazor_Board.Models.Data;

namespace Blazor_Board.Core.Services
{
    public interface IDataService<T>
    {
        public Task<T> Get(int id);
        public Task<List<T>> Get();
        public void Create(List<T> entries);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
