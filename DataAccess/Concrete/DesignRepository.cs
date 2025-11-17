using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class DesignRepository : RepositoryBase<Design, AppDbContext>, IDesignRepository
    {
        public DesignRepository(AppDbContext context) : base(context)
        {
        }
    }
}