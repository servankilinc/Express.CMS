using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Product_
{
    public class ProductUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ProductGroupId { get; set; }
        public string Name { get; set; } = null!;
        public string? FriendlyUrl { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public int Priority { get; set; }

        [IgnoreMap]
        public IFormFile? ImageFile { get; set; }
    }

    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.ProductGroupId).NotNull().WithMessage("Ürün grubu bilgisi boş geçilemez.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}