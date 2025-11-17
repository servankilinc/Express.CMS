using Core.Model;

namespace Model.Entities
{
    public class SubMenu : IEntity, ISoftDeletableEntity, IAuditableEntity, ILoggableEntity
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public string Title { get; set; } = null !;
        public string Url { get; set; } = null !;
        public int Priority { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public virtual Menu? Menu { get; set; }
       
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