using Core.Model;

namespace Model.Entities
{
    public class Link : IEntity, ISoftDeletableEntity, IAuditableEntity, ILoggableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string Icon { get; set; } = null !;
        public string Url { get; set; } = null !;
        public int Priority { get; set; }

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