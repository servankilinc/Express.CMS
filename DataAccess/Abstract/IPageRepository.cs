using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IPageRepository : IRepository<Page>, IRepositoryAsync<Page>
    {
    }
}