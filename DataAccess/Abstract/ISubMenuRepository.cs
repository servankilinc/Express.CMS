using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface ISubMenuRepository : IRepository<SubMenu>, IRepositoryAsync<SubMenu>
    {
    }
}