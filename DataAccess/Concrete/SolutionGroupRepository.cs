using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class SolutionGroupRepository : RepositoryBase<SolutionGroup, AppDbContext>, ISolutionGroupRepository
    {
        public SolutionGroupRepository(AppDbContext context) : base(context)
        {
        }
    }
}