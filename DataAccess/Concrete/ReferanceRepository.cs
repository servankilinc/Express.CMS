using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class ReferanceRepository : RepositoryBase<Referance, AppDbContext>, IReferanceRepository
    {
        public ReferanceRepository(AppDbContext context) : base(context)
        {
        }
    }
}