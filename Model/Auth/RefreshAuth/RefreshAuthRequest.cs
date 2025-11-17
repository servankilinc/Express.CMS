using FluentValidation;

namespace Model.Auth.RefreshAuth
{
    public class RefreshAuthRequest
    {
        public bool IsTrusted { get; set; }
        public string RefreshToken { get; set; } = null !;
        public Guid UserId { get; set; }
    }

    public class RefreshAuthRequestValidator : AbstractValidator<RefreshAuthRequest>
    {
        public RefreshAuthRequestValidator()
        {
            When(b => b.IsTrusted == false, () => RuleFor(b => b.RefreshToken).NotNull().NotEmpty());
            RuleFor(b => b.UserId).NotNull().NotEmpty();
        }
    }
}