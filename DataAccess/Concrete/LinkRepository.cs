using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class LinkRepository : RepositoryBase<Link, AppDbContext>, ILinkRepository
    {
        public LinkRepository(AppDbContext context) : base(context)
        {
        }
    }
}