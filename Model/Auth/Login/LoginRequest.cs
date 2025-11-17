using Core.Utils.CriticalData;
using FluentValidation;

namespace Model.Auth.Login
{
    public class LoginRequest
    {
        public string UserName { get; set; } = null!;

        [CriticalData]
        public string Password { get; set; } = null !;

        public bool RememberMe { get; set; } = false;
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(b => b.UserName).NotNull().NotEmpty();
            RuleFor(b => b.Password).NotNull().MinimumLength(6).NotEmpty();
        }
    }
}