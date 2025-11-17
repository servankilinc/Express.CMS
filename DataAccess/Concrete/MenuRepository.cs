using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class MenuRepository : RepositoryBase<Menu, AppDbContext>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context)
        {
        }
    }
}