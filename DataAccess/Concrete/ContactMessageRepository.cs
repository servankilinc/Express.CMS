using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class ContactMessageRepository : RepositoryBase<ContactMessage, AppDbContext>, IContactMessageRepository
    {
        public ContactMessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}