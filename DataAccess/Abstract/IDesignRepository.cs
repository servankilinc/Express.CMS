using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IDesignRepository : IRepository<Design>, IRepositoryAsync<Design>
    {
    }
}