using Autofac;
using Autofac.Extras.DynamicProxy;
using Core.Utils.CrossCuttingConcerns;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace DataAccess;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserRepository>().As<IUserRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<MenuRepository>().As<IMenuRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<PageRepository>().As<IPageRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<HomeSectionRepository>().As<IHomeSectionRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<DesignRepository>().As<IDesignRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<DetailSectionRepository>().As<IDetailSectionRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<SolutionGroupRepository>().As<ISolutionGroupRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<SubMenuRepository>().As<ISubMenuRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<ProductGroupRepository>().As<IProductGroupRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<ProductRepository>().As<IProductRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<ProjectRepository>().As<IProjectRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<ReferanceGroupRepository>().As<IReferanceGroupRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<ReferanceRepository>().As<IReferanceRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<BlogRepository>().As<IBlogRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<ContactMessageRepository>().As<IContactMessageRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<LinkRepository>().As<ILinkRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<AnnouncementRepository>().As<IAnnouncementRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<SolutionRepository>().As<ISolutionRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<SmtpSettingsRepository>().As<ISmtpSettingsRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();

        builder.RegisterType<RefreshTokenRepository>().As<IRefreshTokenRepository>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(DataAccessExceptionHandlerInterceptor))
            .InstancePerLifetimeScope();
    }
}
