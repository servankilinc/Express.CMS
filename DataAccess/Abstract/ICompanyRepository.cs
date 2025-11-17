using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface ICompanyRepository : IRepository<Company>, IRepositoryAsync<Company>
    {
    }
}