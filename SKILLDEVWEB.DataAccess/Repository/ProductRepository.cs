using SKILLDEVWEB.DataAccess.Data;
using SKILLDEVWEB.DataAccess.Repository.IRepository;
using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
