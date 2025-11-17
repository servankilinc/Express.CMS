using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class DetailSectionRepository : RepositoryBase<DetailSection, AppDbContext>, IDetailSectionRepository
    {
        public DetailSectionRepository(AppDbContext context) : base(context)
        {
        }
    }
}