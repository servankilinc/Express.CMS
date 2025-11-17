using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>, IRepositoryAsync<Product>
    {
    }
}