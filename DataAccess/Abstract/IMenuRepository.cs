using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IMenuRepository : IRepository<Menu>, IRepositoryAsync<Menu>
    {
    }
}