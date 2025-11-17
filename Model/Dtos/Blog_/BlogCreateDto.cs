using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Blog_
{
    public class BlogCreateDto : IDto
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int Priority { get; set; }

        [IgnoreMap]
        public IFormFile ImageFile { get; set; } = null!;
    }

    public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
    {
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long maxFileSize = 15 * 1024 * 1024;
        public BlogCreateDtoValidator()
        {
            RuleFor(v => v.AuthorId).NotNull().WithMessage("Field cannot be null");
            RuleFor(v => v.Title).MinimumLength(2).WithMessage("İsim bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.Content).MinimumLength(30).WithMessage("İçerik bilgisi en az 30 karakter içermeli.");
            RuleFor(x => x.ImageFile).NotNull().WithMessage("Lütfen dosya yükleyiniz.");
            When(x => x.ImageFile != null, () =>
            {
                RuleFor(x => x.ImageFile.Length)
                    .GreaterThan(0).WithMessage("Boş dosya yüklenemez.")
                    .LessThanOrEqualTo(maxFileSize).WithMessage("Dosya en fazla 15 MB olabilir.");

                RuleFor(x => x.ImageFile.FileName)
                    .Must(fileName => permittedExtensions.Contains(Path.GetExtension(fileName).ToLower()))
                    .WithMessage($"Dosya uzantısı desteklenmemektedir.");
            });
        }
    }
}