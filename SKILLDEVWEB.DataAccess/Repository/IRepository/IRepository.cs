using System.Linq.Expressions;

namespace SKILLDEVWEB.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-Category
        IEnumerable<T> GetAll(string? inCludeParametre = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? inCludeParametre = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
