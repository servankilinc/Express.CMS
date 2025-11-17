using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.ProjectEntities;

namespace DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> // DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override DbSet<User> Users { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<HomeSection> HomeSections { get; set; }
    public DbSet<Design> Designs { get; set; }
    public DbSet<DetailSection> DetailSections { get; set; }
    public DbSet<SolutionGroup> SolutionGroups { get; set; }
    public DbSet<SubMenu> SubMenus { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ReferanceGroup> ReferanceGroups { get; set; }
    public DbSet<Referance> Referances { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<SmtpSettings> SmtpSettings { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // Project Entities
    public DbSet<Log> Logs { get; set; }
    public DbSet<Archive> Archives { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Localization> Localizations { get; set; }
    public DbSet<LocalizationLanguageDetail> LocalizationLanguageDetails { get; set; }
    // Project Entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("User");

            u.HasKey(u => u.Id);

            u.HasMany(u => u.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);


            u.HasMany(u => u.RefreshTokens)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            u.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<SmtpSettings>(s =>
        {
            s.ToTable("SmtpSettings");

            s.HasKey(s => s.Id);
        });

        modelBuilder.Entity<Menu>(m =>
        {
            m.ToTable("Menu");

            m.HasKey(m => m.Id);

            m.HasMany(m => m.SubMenuList)
                .WithOne(s => s.Menu)
                .HasForeignKey(s => s.MenuId)
                .OnDelete(DeleteBehavior.NoAction);

            m.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Page>(p =>
        {
            p.ToTable("Page");

            p.HasKey(p => p.Id);

            p.HasOne(p => p.Design)
                .WithOne(d => d.Page)
                .HasForeignKey<Page>(p => p.DesignId)
                .OnDelete(DeleteBehavior.NoAction);

            p.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<HomeSection>(h =>
        {
            h.ToTable("HomeSection");

            h.HasKey(h => h.Id);

            h.HasOne(h => h.Design)
                .WithOne(d => d.HomeSection)
                .HasForeignKey<HomeSection>(h => h.DesignId)
                .OnDelete(DeleteBehavior.NoAction);

            h.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Design>(d =>
        {
            d.ToTable("Design");

            d.HasKey(d => d.Id);

            d.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<DetailSection>(ds =>
        {
            ds.ToTable("DetailSection");

            ds.HasKey(ds => ds.Id);

            ds.HasIndex(ds => ds.ProductId);

            ds.HasOne(ds => ds.Design)
                .WithOne(d => d.DetailSection)
                .HasForeignKey<DetailSection>(ds => ds.DesignId)
                .OnDelete(DeleteBehavior.NoAction);

            ds.HasOne(ds => ds.Product)
                .WithMany(p => p.DetailSections)
                .HasForeignKey(ds => ds.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            ds.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<SolutionGroup>(s =>
        {
            s.ToTable("SolutionGroup");

            s.HasKey(s => s.Id);

            s.HasMany(s => s.Solutions)
                .WithOne(s => s.SolutionGroup)
                .HasForeignKey(s => s.SolutionGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            s.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<SubMenu>(s =>
        {
            s.ToTable("SubMenu");

            s.HasKey(s => s.Id);

            s.HasOne(s => s.Menu)
                .WithMany(s => s.SubMenuList)
                .HasForeignKey(s => s.MenuId)
                .OnDelete(DeleteBehavior.NoAction);

            s.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<ProductGroup>(p =>
        {
            p.ToTable("ProductGroup");

            p.HasKey(p => p.Id);

            p.HasMany(p => p.Products)
                .WithOne(p => p.ProductGroup)
                .HasForeignKey(p => p.ProductGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            p.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable("Product");

            p.HasKey(p => p.Id);

            p.HasOne(p => p.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            p.HasOne(p => p.Design)
                .WithOne(d => d.Product)
                .HasForeignKey<Product>(p => p.DesignId)
                .OnDelete(DeleteBehavior.NoAction);

            p.HasMany(p => p.DetailSections)
                .WithOne(ds => ds.Product)
                .HasForeignKey(ds => ds.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            p.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Project>(p =>
        {
            p.ToTable("Project");

            p.HasKey(p => p.Id);

            p.HasOne(p => p.Design)
                .WithOne(d => d.Project)
                .HasForeignKey<Project>(p => p.DesignId)
                .OnDelete(DeleteBehavior.NoAction);

            p.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<ReferanceGroup>(r =>
        {
            r.ToTable("ReferanceGroup");

            r.HasKey(r => r.Id);

            r.HasMany(r => r.Referances)
                .WithOne(r => r.ReferanceGroup)
                .HasForeignKey(r => r.ReferanceGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            r.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Referance>(r =>
        {
            r.ToTable("Referance");

            r.HasKey(r => r.Id);

            r.HasOne(r => r.ReferanceGroup)
                .WithMany(r => r.Referances)
                .HasForeignKey(r => r.ReferanceGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            r.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Blog>(b =>
        {
            b.ToTable("Blog");

            b.HasKey(b => b.Id);

            b.HasOne(b => b.Author)
                .WithMany(b => b.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<ContactMessage>(c =>
        {
            c.ToTable("ContactMessage");

            c.HasKey(c => c.Id);

            c.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Link>(l =>
        {
            l.ToTable("Link");

            l.HasKey(l => l.Id);

            l.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Announcement>(a =>
        {
            a.ToTable("Announcement");

            a.HasKey(a => a.Id);

            a.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Company>(c =>
        {
            c.ToTable("Company");

            c.HasKey(c => c.Id);

            c.HasQueryFilter(f => !f.IsDeleted);
        });

        modelBuilder.Entity<Solution>(s =>
        {
            s.ToTable("Solution");

            s.HasKey(s => s.Id);

            s.HasOne(s => s.SolutionGroup)
                .WithMany(s => s.Solutions)
                .HasForeignKey(s => s.SolutionGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            s.HasOne(s => s.Design)
                .WithOne(d => d.Solution)
                .HasForeignKey<Solution>(s => s.DesignId)
                .OnDelete(DeleteBehavior.Cascade);

            s.HasQueryFilter(f => !f.IsDeleted);
        });


        modelBuilder.Entity<RefreshToken>(r =>
        {
            r.HasKey(r => r.Id);

            r.HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<Log>(l =>
        {
            l.ToTable("ProjectLogs");

            l.HasKey(l => l.Id);

            l.HasIndex(l => l.EntityId);
        });

        modelBuilder.Entity<Archive>(a =>
        {
            a.ToTable("ProjectArchives");

            a.HasKey(a => a.Id);

            a.HasIndex(a => a.EntityId);
        });

        modelBuilder.Entity<Language>(l =>
        {
            l.ToTable("ProjectLanguages");

            l.HasKey(l => l.Id);

            l.HasMany(l => l.LocalizationLanguageDetails)
                .WithOne(lld => lld.Language)
                .HasForeignKey(lld => lld.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Localization>(l =>
        {
            l.ToTable("ProjectLocalizations");
            l.HasKey(l => l.Id);

            l.HasMany(l => l.LocalizationLanguageDetails)
                .WithOne(lld => lld.Localization)
                .HasForeignKey(lld => lld.LocalizationId)
                .OnDelete(DeleteBehavior.Cascade);

            l.HasIndex(l => l.EntityId);
        });
        
        modelBuilder.Entity<LocalizationLanguageDetail>(lld =>
        {
            lld.ToTable("ProjectLocalizationLanguageDetails");

            lld.HasKey(lld => new { lld.LocalizationId, lld.LanguageId });
        });

        modelBuilder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("Roles");

            entity.HasData(
                new
                {
                    Id = new Guid("b370875e-34cd-4b79-891c-93ae38f99d11"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "b370875e-34cd-4b79-891c-93ae38f99d11"
                },
                new
                {
                    Id = new Guid("cd6040ef-dacc-4678-9a85-154f12581cff"),
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = "cd6040ef-dacc-4678-9a85-154f12581cff"
                },
                new
                {
                    Id = new Guid("7138ec51-4f9e-4afd-b61b-5a9a4584f5da"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "7138ec51-4f9e-4afd-b61b-5a9a4584f5da"
                },
                new
                {
                    Id = new Guid("1f20c152-530e-4064-a39c-bbbed341fe84"),
                    Name = "Owner",
                    NormalizedName = "OWNER",
                    ConcurrencyStamp = "1f20c152-530e-4064-a39c-bbbed341fe84"
                }
            );
        });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });

        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });

        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
    }
}
