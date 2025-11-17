using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Model.Dtos.Design_;

namespace Model.Dtos.Product_
{
    public class ProductCreateDto : IDto
    {
        public Guid ProductGroupId { get; set; }
        public string Name { get; set; } = null !;
        public string? FriendlyUrl { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; } = null !;
        public int Priority { get; set; }

        [IgnoreMap]
        public IFormFile ImageFile { get; set; } = null!;

        public DesignCreateDto? DesignCreateModel { get; set; }
    }

    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long maxFileSize = 15 * 1024 * 1024;

        public ProductCreateDtoValidator()
        {
            RuleFor(v => v.ProductGroupId).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
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