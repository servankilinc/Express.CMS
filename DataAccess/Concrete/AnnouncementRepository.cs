using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class AnnouncementRepository : RepositoryBase<Announcement, AppDbContext>, IAnnouncementRepository
    {
        public AnnouncementRepository(AppDbContext context) : base(context)
        {
        }
    }
}