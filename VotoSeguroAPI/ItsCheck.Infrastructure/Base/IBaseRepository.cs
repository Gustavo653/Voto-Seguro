using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ItsCheck.Infrastructure.Base
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetEntities();
        IQueryable<T> GetTrackedEntities();
        Task<IEnumerable<T>> GetList();
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entity);
        Task InsertAsync(T entity);
        void Update(T entity);
        void UpdateRange(T[] entity);
        void Delete(T entity);
        void DeleteRange(T[] entity);
        bool SaveChanges();
        IEnumerable<EntityEntry> GetChanges();
        void SeeChanges();
        void ClearChanges();
        Task<bool> SaveChangesAsync();
        Task<List<T>> GetListAsync();
        void Attach(T entity);
        void Detach(T entity);
        void AttachRange(IEnumerable<T> entity);
    }
}
