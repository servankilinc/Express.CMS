using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Company_
{
    public class CompanyUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Since { get; set; } = null!;
        public string? Logo { get; set; }
        public string Address { get; set; } = null !;
        public string EmailAdresses { get; set; } = null !;
        public string PhoneNumbers { get; set; } = null !;
        public string? FaxNumbers { get; set; }
        public string WorkingStartTime { get; set; } = null !;
        public string WorkingEndTime { get; set; } = null !;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? GapesJSLicenseKey { get; set; }
        public string? CKEditorLicenseKey { get; set; }

        [IgnoreMap]
        public IFormFile? ImageFile { get; set; }
    }

    public class CompanyUpdateDtoValidator : AbstractValidator<CompanyUpdateDto>
    {
        public CompanyUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Since).NotNull().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Address).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.EmailAdresses).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.PhoneNumbers).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.WorkingStartTime).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.WorkingEndTime).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            //RuleFor(v => v.GapesJSLicenseKey).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            //RuleFor(v => v.CKEditorLicenseKey).NotEmpty().WithMessage("Bu alan boş geçilemez.");
        }
    }
}