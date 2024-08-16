using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);

    }
}
