using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Abstract
{
    public interface IContactMessageRepository : IRepository<ContactMessage>, IRepositoryAsync<ContactMessage>
    {
    }
}