using Core.Model;
using FluentValidation;

namespace Model.Dtos.Announcement_
{
    public class AnnouncementCreateDto : IDto
    {
        public string Title { get; set; } = null !;
        public string Message { get; set; } = null !;
    }

    public class AnnouncementCreateDtoValidator : AbstractValidator<AnnouncementCreateDto>
    {
        public AnnouncementCreateDtoValidator()
        {
            RuleFor(v => v.Title).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Message).NotEmpty().WithMessage("Bu alan boş geçilemez.");
        }
    }
}