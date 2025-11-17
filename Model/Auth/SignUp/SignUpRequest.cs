using FluentValidation;

namespace Model.Auth.SignUp
{
    public class SignUpRequest
    {
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null !;
    }

    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(b => b.UserName).NotNull().NotEmpty();
            RuleFor(b => b.Password).NotNull().MinimumLength(6).NotEmpty();
            RuleFor(b => b.FullName).NotNull().NotEmpty();
        }
    }
}