using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Blog_
{
    public class BlogUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Image { get; set; } = null!;

        [IgnoreMap]
        public IFormFile? ImageFile { get; set; }
    }

    public class BlogUpdateDtoValidator : AbstractValidator<BlogUpdateDto>
    {
        public BlogUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.AuthorId).NotNull().WithMessage("Yazar bilgisi boş geçilemez.");
            RuleFor(v => v.Title).MinimumLength(2).WithMessage("İsim bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.Content).MinimumLength(30).WithMessage("İçerik bilgisi en az 30 karakter içermeli.");
        }
    }
}