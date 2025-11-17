using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class SolutionRepository : RepositoryBase<Solution, AppDbContext>, ISolutionRepository
    {
        public SolutionRepository(AppDbContext context) : base(context)
        {
        }
    }
}