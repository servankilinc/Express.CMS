using Core.Model;

namespace Model.Entities
{
    public class Design : IEntity, ISoftDeletableEntity, IArchivableEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string? Html { get; set; }
        public string? Css { get; set; }
        public string? Script { get; set; }
        public string? ProjectJson { get; set; }


        public virtual Page? Page { get; set; }
        public virtual HomeSection? HomeSection { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Solution? Solution { get; set; }
        public virtual Project? Project { get; set; }
        public virtual DetailSection? DetailSection { get; set; }

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