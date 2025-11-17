using AutoMapper;
using Business.Abstract;
using Business.ServiceBase;
using Core.BaseRequestModels;
using Core.Utils.CrossCuttingConcerns;
using Core.Utils.Datatable;
using Core.Utils.ExceptionHandle.Exceptions;
using Core.Utils.HttpContextManager;
using DataAccess.Abstract;
using Model.Dtos.ContactMessage_;
using Model.Entities;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace Business.Concrete
{
    [ExceptionHandler]
    public class ContactMessageService : ServiceBase<ContactMessage, IContactMessageRepository>, IContactMessageService
    {
        private readonly ISmtpSettingsService _smtpSettingsService;
        private readonly HttpContextManager _httpContextManager;
        public ContactMessageService(IContactMessageRepository contactMessageRepository, IMapper mapper, ISmtpSettingsService smtpSettingsService, HttpContextManager httpContextManager) : base(contactMessageRepository, mapper)
        {
            _smtpSettingsService = smtpSettingsService;
            _httpContextManager = httpContextManager;
        }

        [Validation(typeof(ContactMessageCreateDto))]
        public async Task<ContactMessage> CreateAsync(ContactMessageCreateDto contactMessage, CancellationToken cancellationToken = default)
        {
            try
            {
                contactMessage.ClientIp = _httpContextManager.GetClientIp();
            }
            catch (Exception e)
            {
                Log.ForContext("Target", "Business").Error("Mesaj İşleme Bloğunda Kullanıcı Ip Bilgisi Okunamadı", e);
            }
            contactMessage.NormalizedEmail = contactMessage.Email.Trim().ToLowerInvariant();
            
            // Mail Limit Kontrol
            DateTime toDay = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            var countOldMessages = 0;
            if (string.IsNullOrEmpty(contactMessage.ClientIp))
            {
                countOldMessages = await _CountAsync(where: f => (f.NormalizedEmail == contactMessage.NormalizedEmail) && f.CreateDateUtc > toDay);
            }
            else
            {
                countOldMessages = await _CountAsync(where: f => (f.NormalizedEmail == contactMessage.NormalizedEmail || (f.ClientIp != null && f.ClientIp != "" && f.ClientIp == contactMessage.ClientIp)) && f.CreateDateUtc > toDay);
            }

            if (countOldMessages > 100) throw new BusinessException("Bugün İçin Mesaj Gönderim Sınırına Ulaştınız");

            var resultSend = false;
            try
            {
                var smtpSettings = await _smtpSettingsService.GetAsync();
                if (smtpSettings == null) throw new GeneralException("SmtpSettings'e ulaşılamıyor");

                using var message = new MailMessage();
                message.To.Add(smtpSettings.InformationEmailAddress); // alıcı mail (bilgi.express)
                message.From = new MailAddress(smtpSettings.EmailAddress); // form.Email (Gönderen noreply)
                message.Subject = contactMessage.Subject;
                message.Body = GenerateHtml(contactMessage);
                message.IsBodyHtml = true;

                using var client = new SmtpClient(smtpSettings.OutgoingServerHost.ToString(), smtpSettings.OutgoingServerPort)
                {
                    Credentials = new NetworkCredential(smtpSettings.EmailAddress, smtpSettings.Password),
                    EnableSsl = smtpSettings.SslEnable
                };

                await client.SendMailAsync(message);
                resultSend = true;
            }
            catch (Exception)
            {
                resultSend = false;
            }
            contactMessage.SendingStatus = resultSend;
            var result = await _AddAsync(contactMessage, cancellationToken);
            return result;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _DeleteAsync(where: (f) => f.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<DatatableResponseServerSide<ContactMessage>> DatatableServerSideAsync(DynamicDatatableServerSideRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _DatatableServerSideAsync(datatableRequest: request.GetDatatableRequest(), filter: request.Filter, cancellationToken: cancellationToken);
            return result;
        }



        private string GenerateHtml(ContactMessageCreateDto contactMessage)
        {
            return @$"<!doctype html>
<html lang=""tr"">
<head>
  <meta charset=""utf-8"">
  <meta name=""viewport"" content=""width=device-width,initial-scale=1"">
</head>
<body style=""margin:0;padding:0;background-color:#f4f4f6;"">
  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
    <tr>
      <td align=""center"" style=""padding:20px 10px;"">
        <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""max-width:600px;background:#ffffff;border-radius:8px;overflow:hidden;"">
          <tr>
            <td style=""padding:20px 30px;background:#0d6efd;color:#ffffff;font-family:Arial,Helvetica,sans-serif;"">
              <h1 style=""margin:0;font-size:20px;"">
                Express Bilgisayar 
                <span style=""font-weight: 100; font-size:17px"">Mail Sağlayıcısı</span> 
              </h1>
            </td>
          </tr>
          <tr>
            <td style=""padding:24px 30px;font-family:Arial,Helvetica,sans-serif;color:#333333;line-height:1.5;"">
              <p style=""margin-bottom:12px;font-size:14px;"">
                <strong>İsim-Soyisim:</strong>
                <span>{contactMessage.FullName}</span>
              </p>
              <p style=""margin-bottom:12px; font-size:14px;"">
                <strong>E-posta:</strong>
                <span>{contactMessage.Email}</span>
              </p>

              <h2 style=""font-size:16px; margin:12px 0; color:#0b5ed7;"">
              	<strong>Konu: </strong> 
                <span>{contactMessage.Subject}</span>
              </h2>

              <div style=""font-size:14px; margin-bottom:18px;"">
                <strong>Mesaj: </strong>
                <span style=""padding: 6px"">{contactMessage.Message}</span>
              </div>

              <hr style=""border:none;border-top:1px solid #e9ecef;margin:18px 0;"">

              <p style=""font-size:13px;color:#6c757d;margin:0;"">
                Bu mesaj Express Bilgisayar Web Sitesi Üzerinden Yönlendirilmiştir.
              </p>
            </td>
          </tr>
          <tr>
            <td style=""padding:16px 30px;background:#f8f9fa;font-family:Arial,Helvetica,sans-serif;color:#6c757d;font-size:12px;"">
              <p style=""margin:0;"">
                © Express Bilgisayar. Tüm hakları saklıdır.
              </p>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
        }
    }
}