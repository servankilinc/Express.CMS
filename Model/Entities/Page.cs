using Core.Model;

namespace Model.Entities
{
    public class Page : IEntity, ISoftDeletableEntity, IArchivableEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid? DesignId { get; set; }
        public string Name { get; set; } = null!;
        public string PathName { get; set; } = null!;
        public int Priority { get; set; }
        public bool? ShowFooter { get; set; }
        public string? Tags { get; set; }
        public virtual Design? Design { get; set; }

        #region Inherited Props
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreateDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateUtc { get; set; }
        #endregion

        public ICollection<string> GetTagList()
        {
            if (string.IsNullOrWhiteSpace(Tags))
            {
                return new List<string>();
            }
            return Tags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }
    }
}