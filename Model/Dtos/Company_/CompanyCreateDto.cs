using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Company_
{
    public class CompanyCreateDto : IDto
    {
        public string Since { get; set; } = null!;
        public string? Logo { get; set; }
        public string Address { get; set; } = null!;
        public string EmailAdresses { get; set; } = null!;
        public string PhoneNumbers { get; set; } = null!;
        public string? FaxNumbers { get; set; }
        public string WorkingStartTime { get; set; } = null!;
        public string WorkingEndTime { get; set; } = null!;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? GapesJSLicenseKey { get; set; }
        public string? CKEditorLicenseKey { get; set; }

        [IgnoreMap]
        public IFormFile ImageFile { get; set; } = null!;
    }

    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long maxFileSize = 15 * 1024 * 1024;

        public CompanyCreateDtoValidator()
        {
            RuleFor(v => v.Since).NotNull().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Address).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.EmailAdresses).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.PhoneNumbers).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.WorkingStartTime).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.WorkingEndTime).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            //RuleFor(v => v.GapesJSLicenseKey).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            //RuleFor(v => v.CKEditorLicenseKey).NotEmpty().WithMessage("Bu alan boş geçilemez.");

            RuleFor(x => x.ImageFile).NotNull().WithMessage("Lütfen dosya yükleyiniz.");
            When(x => x.ImageFile != null, () =>
            {
                RuleFor(x => x.ImageFile.Length)
                    .GreaterThan(0).WithMessage("Boç dosya yüklenemez.")
                    .LessThanOrEqualTo(maxFileSize).WithMessage("Dosya en fazla 15 MB olabilir.");

                RuleFor(x => x.ImageFile.FileName)
                    .Must(fileName => permittedExtensions.Contains(Path.GetExtension(fileName).ToLower()))
                    .WithMessage($"Dosya uzantısı desteklenmemektedir.");
            });
        }
    }
}