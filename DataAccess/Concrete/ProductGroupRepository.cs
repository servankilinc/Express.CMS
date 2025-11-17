using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class ProductGroupRepository : RepositoryBase<ProductGroup, AppDbContext>, IProductGroupRepository
    {
        public ProductGroupRepository(AppDbContext context) : base(context)
        {
        }
    }
}