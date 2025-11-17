using Core.Model;

namespace Model.Dtos.Design_
{
    public class DesignDto : IDto
    {
        public Guid Id { get; set; }
        public string? Html { get; set; }
        public string? Css { get; set; }
        public string? Script { get; set; }
        public string? ProjectJson { get; set; }
    }

    public class DesignRenderDto : IDto
    {
        public Guid Id { get; set; }
        public string? Html { get; set; }
        public string? Css { get; set; }
        public string? Script { get; set; }
    }
}