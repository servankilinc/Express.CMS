using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IDetailSectionRepository : IRepository<DetailSection>, IRepositoryAsync<DetailSection>
    {
    }
}