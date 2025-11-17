using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Contexts;
using DataAccess.Repository;
using Model.Entities;

namespace DataAccess.Concrete
{
    [DataAccessException]
    public class SmtpSettingsRepository : RepositoryBase<SmtpSettings, AppDbContext>, ISmtpSettingsRepository
    {
        public SmtpSettingsRepository(AppDbContext context) : base(context)
        {
        }
    }
}