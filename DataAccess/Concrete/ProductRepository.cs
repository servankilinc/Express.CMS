using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class ProductRepository : RepositoryBase<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}