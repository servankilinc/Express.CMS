using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using Model.Entities;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class SmtpSettingsService : ServiceBase<SmtpSettings, ISmtpSettingsRepository>, ISmtpSettingsService
    {
        public SmtpSettingsService(ISmtpSettingsRepository smtpSettingsRepository, IMapper mapper) : base(smtpSettingsRepository, mapper)
        {
        }

        public SmtpSettings? Get()
        {
            var result =  _Get(tracking: false);
            return result;
        }

        public async Task<SmtpSettings?> GetAsync(CancellationToken cancellationToken = default)
        {
            var result = await _GetAsync(tracking: false, cancellationToken: cancellationToken);
            return result;
        } 

        [Validation(typeof(SmtpSettings))]
        public async Task<SmtpSettings> UpdateAsync(SmtpSettings request, CancellationToken cancellationToken)
        {
            var isExist = await _IsExistAsync(where: f=> f.Id == request.Id, cancellationToken: cancellationToken);
            if(isExist == true)
            {
                var result = await _UpdateAsync(request, where: (f) => f.Id == request.Id, cancellationToken: cancellationToken);
                return result;
            }
            else
            {
                var result = await _AddAsync(request, cancellationToken: cancellationToken);
                return result;
            }
        } 
    }
}