using DataAccess.Abstract;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;
        public IUserRepository Users { get; private set; }
        public IMenuRepository Menus { get; private set; }
        public IPageRepository Pages { get; private set; }
        public IHomeSectionRepository HomeSections { get; private set; }
        public IDesignRepository Designs { get; private set; }
        public IDetailSectionRepository DetailSections { get; private set; }
        public ISolutionGroupRepository SolutionGroups { get; private set; }
        public ISubMenuRepository SubMenus { get; private set; }
        public IProductGroupRepository ProductGroups { get; private set; }
        public IProductRepository Products { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IReferanceGroupRepository ReferanceGroups { get; private set; }
        public IReferanceRepository Referances { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public IContactMessageRepository ContactMessages { get; private set; }
        public ILinkRepository Links { get; private set; }
        public IAnnouncementRepository Announcements { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public ISolutionRepository Solutions { get; private set; }
        public ISmtpSettingsRepository SmtpSettings { get; private set; }
        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IMenuRepository menuRepository, IPageRepository pageRepository, IHomeSectionRepository homeSectionRepository, IDesignRepository designRepository, IDetailSectionRepository detailSectionRepository, ISolutionGroupRepository solutionGroupRepository, ISubMenuRepository subMenuRepository, IProductGroupRepository productGroupRepository, IProductRepository productRepository, IProjectRepository projectRepository, IReferanceGroupRepository referanceGroupRepository, IReferanceRepository referanceRepository, IBlogRepository blogRepository, IContactMessageRepository contactMessageRepository, ILinkRepository linkRepository, IAnnouncementRepository announcementRepository, ICompanyRepository companyRepository, ISolutionRepository solutionRepository, ISmtpSettingsRepository smtpSettingsRepository,  IRefreshTokenRepository refreshTokens)
        {
            _context = context;
            Users = userRepository;
            Menus = menuRepository;
            Pages = pageRepository;
            HomeSections = homeSectionRepository;
            Designs = designRepository;
            DetailSections = detailSectionRepository;
            SolutionGroups = solutionGroupRepository;
            SubMenus = subMenuRepository;
            ProductGroups = productGroupRepository;
            Products = productRepository;
            Projects = projectRepository;
            ReferanceGroups = referanceGroupRepository;
            Referances = referanceRepository;
            Blogs = blogRepository;
            ContactMessages = contactMessageRepository;
            Links = linkRepository;
            Announcements = announcementRepository;
            Companies = companyRepository;
            Solutions = solutionRepository;
            SmtpSettings = smtpSettingsRepository;
            RefreshTokens = refreshTokens;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started for begin transaction.");
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started for commit transaction.");
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started for begin transaction.");
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started for commit.");
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            await _context.DisposeAsync();
        }
    }
}