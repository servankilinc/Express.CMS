using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IHomeSectionRepository : IRepository<HomeSection>, IRepositoryAsync<HomeSection>
    {
    }
}