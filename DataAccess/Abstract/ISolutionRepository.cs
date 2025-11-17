using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface ISolutionRepository : IRepository<Solution>, IRepositoryAsync<Solution>
    {
    }
}