using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IProjectRepository : IRepository<Project>, IRepositoryAsync<Project>
    {
    }
}