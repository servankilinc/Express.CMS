using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IProductGroupRepository : IRepository<ProductGroup>, IRepositoryAsync<ProductGroup>
    {
    }
}