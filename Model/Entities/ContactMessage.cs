using Core.Model;

namespace Model.Entities
{
    public class ContactMessage : IEntity, ISoftDeletableEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null !;
        public string Email { get; set; } = null !;
        public string NormalizedEmail { get; set; } = null!;
        public string? ClientIp { get; set; }
        public string Subject { get; set; } = null !;
        public string Message { get; set; } = null !;
        public bool SendingStatus { get; set; }

        #region Inherited Props
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreateDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateUtc { get; set; } 
        #endregion
    }
}