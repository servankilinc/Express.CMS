using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IAnnouncementRepository : IRepository<Announcement>, IRepositoryAsync<Announcement>
    {
    }
}