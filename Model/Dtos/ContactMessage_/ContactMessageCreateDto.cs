using Core.Model;
using FluentValidation;

namespace Model.Dtos.ContactMessage_
{
    public class ContactMessageCreateDto : IDto
    {
        public string FullName { get; set; } = null !;
        public string Email { get; set; } = null !;
        public string Subject { get; set; } = null !;
        public string Message { get; set; } = null !;

        public string NormalizedEmail { get; set; } = null!;
        public string? ClientIp { get; set; }
        public bool SendingStatus { get; set; }
    }

    public class ContactMessageCreateDtoValidator : AbstractValidator<ContactMessageCreateDto>
    {
        public ContactMessageCreateDtoValidator()
        {
            RuleFor(v => v.FullName).NotEmpty().WithMessage("İsim bilgisi en az 3 karakter içermeli.");
            RuleFor(v => v.FullName).MinimumLength(3).WithMessage("İsim bilgisi en az 3 karakter içermeli.");
            RuleFor(v => v.Email).NotEmpty().WithMessage("Lütfen geçerli bir e-posta adresi griniz.");
            RuleFor(v => v.Email).EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi griniz.");
            RuleFor(v => v.Subject).NotEmpty().WithMessage("Konu bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.Subject).MinimumLength(2).WithMessage("Konu bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.Subject).MaximumLength(150).WithMessage("Konu bilgisi en fazla 150 karakter içermeli.");
            RuleFor(v => v.Message).NotEmpty().WithMessage("Mesaj bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.Message).MinimumLength(10).WithMessage("Mesaj bilgisi en az 10 karakter içermeli.");
            RuleFor(v => v.Message).MaximumLength(500).WithMessage("Mesaj bilgisi en fazla 500 karakter içermeli.");
        }
    }
}