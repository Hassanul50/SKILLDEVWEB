using Microsoft.EntityFrameworkCore;
using SKILLDEVWEB.DataAccess.Data;
using SKILLDEVWEB.DataAccess.Repository.IRepository;
using System.Linq.Expressions;

namespace SKILLDEVWEB.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //DbContext _dbContext;
        //public Repository(DbContext db)
        //{
        //    _dbContext = db;
        //}

        //private DbSet<T> Table 
        //{
        //    get { return _dbContext.Set<T>(); }
        //}

        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this._dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> querry = _dbset;
            return querry.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
    }
}
