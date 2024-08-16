using SKILLDEVWEB.DataAccess.Data;
using SKILLDEVWEB.DataAccess.Repository.IRepository;
using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
