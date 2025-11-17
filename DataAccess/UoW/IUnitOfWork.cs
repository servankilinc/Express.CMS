using DataAccess.Abstract;

namespace DataAccess.UoW
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IUserRepository Users { get; }
        IMenuRepository Menus { get; }
        IPageRepository Pages { get; }
        IHomeSectionRepository HomeSections { get; }
        IDesignRepository Designs { get; }
        IDetailSectionRepository DetailSections { get; }
        ISolutionGroupRepository SolutionGroups { get; }
        ISubMenuRepository SubMenus { get; }
        IProductGroupRepository ProductGroups { get; }
        IProductRepository Products { get; }
        IProjectRepository Projects { get; }
        IReferanceGroupRepository ReferanceGroups { get; }
        IReferanceRepository Referances { get; }
        IBlogRepository Blogs { get; }
        IContactMessageRepository ContactMessages { get; }
        ILinkRepository Links { get; }
        IAnnouncementRepository Announcements { get; }
        ICompanyRepository Companies { get; }
        ISolutionRepository Solutions { get; }
        ISmtpSettingsRepository SmtpSettings { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}