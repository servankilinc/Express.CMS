using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IReferanceGroupRepository : IRepository<ReferanceGroup>, IRepositoryAsync<ReferanceGroup>
    {
    }
}