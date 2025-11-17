using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;

namespace Model.Dtos.User_
{
    public class UserUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null !;
        public string UserName { get; set; } = null!;
        public List<string>? RoleList { get; set; }
        
        [CriticalData]
        public string OldPassword { get; set; } = null!;

        [CriticalData]
        public string? NewPassword { get; set; }
    }

    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.FullName).MinimumLength(2).WithMessage("İsim bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.UserName).MinimumLength(2).WithMessage("Kullanıcı Ad bilgisi en az 2 karakter olmalı.");
            RuleFor(v => v.OldPassword).NotNull().WithMessage("Password cannot be null.");
            RuleFor(v => v.OldPassword).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(v => v.NewPassword).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(v => v.NewPassword)
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .When(v => !string.IsNullOrEmpty(v.NewPassword));
        }
    }
}