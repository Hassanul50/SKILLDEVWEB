using SKILLDEVWEB.Model.Models;

namespace SKILLDEVWEB.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
