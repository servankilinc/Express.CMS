using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;

namespace Model.Dtos.User_
{
    public class UserCreateDto : IDto
    {
        public string FullName { get; set; } = null !;
        public string UserName { get; set; } = null!;
        [CriticalData]
        public string Password { get; set; } = null !;
        public List<string>? RoleList { get; set; }
    }

    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(v => v.FullName).MinimumLength(2).WithMessage("İsim bilgisi en az 2 karakter olmalı.");
            RuleFor(v => v.UserName).MinimumLength(2).WithMessage("Kullanıcı Ad bilgisi en az 2 karakter olmalı.");
            RuleFor(v => v.Password).NotNull().WithMessage("Password cannot be null.");
            RuleFor(v => v.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}