using Business.ServiceBase;
using Model.Entities;

namespace Business.Abstract
{
    public interface ISmtpSettingsService : IServiceBase<SmtpSettings>, IServiceBaseAsync<SmtpSettings>
    {
        SmtpSettings? Get();
        Task<SmtpSettings?> GetAsync(CancellationToken cancellationToken = default);
        Task<SmtpSettings> UpdateAsync(SmtpSettings request, CancellationToken cancellationToken = default);
    }
}