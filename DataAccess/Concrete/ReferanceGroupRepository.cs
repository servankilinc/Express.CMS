using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class ReferanceGroupRepository : RepositoryBase<ReferanceGroup, AppDbContext>, IReferanceGroupRepository
    {
        public ReferanceGroupRepository(AppDbContext context) : base(context)
        {
        }
    }
}