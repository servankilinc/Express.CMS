using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface ILinkRepository : IRepository<Link>, IRepositoryAsync<Link>
    {
    }
}