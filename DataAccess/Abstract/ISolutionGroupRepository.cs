using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface ISolutionGroupRepository : IRepository<SolutionGroup>, IRepositoryAsync<SolutionGroup>
    {
    }
}