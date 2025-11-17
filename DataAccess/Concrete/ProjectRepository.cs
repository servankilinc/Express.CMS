using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class ProjectRepository : RepositoryBase<Project, AppDbContext>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}