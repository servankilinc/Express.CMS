using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Referance_
{
    public class ReferanceCreateDto : IDto
    {
        public Guid ReferanceGroupId { get; set; }
        public string Name { get; set; } = null !;
        public string Image { get; set; } = null !;
        
        [IgnoreMap]
        public IFormFile ImageFile { get; set; } = null!;
    }

    public class ReferanceCreateDtoValidator : AbstractValidator<ReferanceCreateDto>
    {
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" , ".webp" };
        private const long maxFileSize = 15 * 1024 * 1024;

        public ReferanceCreateDtoValidator()
        {
            RuleFor(v => v.ReferanceGroupId).NotNull().WithMessage("Referans grub bilgisi boş geçilemez.");
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