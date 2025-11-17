using Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Model.Entities
{
    public class User : IdentityUser<Guid>, IEntity, ISoftDeletableEntity, IAuditableEntity
    {
        public override Guid Id { get; set; }
        public string FullName { get; set; } = null !;
        //public override string UserName { get; set; } = null!;
        public virtual ICollection<Blog>? Blogs { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }

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