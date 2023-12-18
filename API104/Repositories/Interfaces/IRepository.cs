using System.Linq.Expressions;
namespace API104.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool IsDescending = false,
            int skip = 0,
            int take = 0,
            bool IsTracking = true,
            params string[] includes);

        //--------------OrderBy ve OrderByDescending ucun ayrica bir method------------
        //Task<IQueryable<T>> OrderByAsync<TKey>(Expression<Func<T, object>> orderExpression); 
        //Task<IQueryable<T>> OrderByDescendingAsync<TKey>(Expression<Func<T, object>> orderExpression );
        //-----------------------------------------------------------------------------

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
