using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class SubMenuRepository : RepositoryBase<SubMenu, AppDbContext>, ISubMenuRepository
    {
        public SubMenuRepository(AppDbContext context) : base(context)
        {
        }
    }
}