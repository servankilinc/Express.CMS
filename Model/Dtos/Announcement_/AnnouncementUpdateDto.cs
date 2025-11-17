using Core.Model;
using FluentValidation;

namespace Model.Dtos.Announcement_
{
    public class AnnouncementUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null !;
        public string Message { get; set; } = null !;
    }

    public class AnnouncementUpdateDtoValidator : AbstractValidator<AnnouncementUpdateDto>
    {
        public AnnouncementUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Title).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Message).NotEmpty().WithMessage("Bu alan boş geçilemez.");
        }
    }
}