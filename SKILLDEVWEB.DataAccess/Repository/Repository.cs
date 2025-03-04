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
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public IEnumerable<T> GetAll(string? inCludeParametre = null)
        {
            IQueryable<T> querry = _dbset;
            if (!string.IsNullOrEmpty(inCludeParametre))
            {
                foreach (var inclueprop in inCludeParametre.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(inclueprop);
                }
            }
            return querry.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? inCludeParametre = null)
        {
            IQueryable<T> query = _dbset;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(inCludeParametre))
            {
                foreach (var inclueprop in inCludeParametre.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inclueprop);
                }
            }
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
