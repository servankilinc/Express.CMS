using Core.Model;
using FluentValidation;

namespace Model.Entities
{
    public class SmtpSettings: IEntity
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; } = null!; // formu dolduran kullanıcının kendi mail adresi
        public string InformationEmailAddress { get; set; } = null!;
        public string IncomingServerHost { get; set; } = null!;
        public string OutgoingServerHost { get; set; } = null!;
        public int IncomingServerPort { get; set; }
        public int OutgoingServerPort { get; set; }
        public string Password { get; set; } = null!;
        public bool SslEnable { get; set; }
    }

    public class SmtpSettingsValidator : AbstractValidator<SmtpSettings>
    {
        public SmtpSettingsValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.EmailAddress).NotNull().EmailAddress().WithMessage("Email bilgisi boş geçilemez.");
            RuleFor(v => v.InformationEmailAddress).NotNull().EmailAddress().WithMessage("Email bilgisi boş geçilemez.");
            RuleFor(v => v.IncomingServerHost).NotNull().WithMessage("Bu alan zorunlu.");
            RuleFor(v => v.OutgoingServerHost).NotNull().WithMessage("Bu alan zorunlu.");
            RuleFor(v => v.IncomingServerPort).NotNull().WithMessage("Bu alan zorunlu.");
            RuleFor(v => v.OutgoingServerPort).NotNull().WithMessage("Bu alan zorunlu.");
            RuleFor(v => v.Password).NotEmpty().MinimumLength(3).WithMessage("Şifre bilgisi en az 3 karakter içermeli.");
        }
    }
}
