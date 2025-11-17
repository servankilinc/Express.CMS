using Core.Model;

namespace Model.Entities
{
    public class DetailSection : IEntity, ISoftDeletableEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int Priority { get; set; }

        public Guid? DesignId { get; set; }
        public virtual Design? Design { get; set; }
        
        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

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