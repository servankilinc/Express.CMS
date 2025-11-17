using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.Utils.TokenService;
using Core.Utils.CrossCuttingConcerns;

namespace Business;

public class AutofacModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<TokenService>().As<ITokenService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ExceptionHandlerInterceptor))
			.InstancePerLifetimeScope();

        builder.RegisterType<AuthService>().As<IAuthService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor))
			.InstancePerLifetimeScope();

		// ***** Entity Services *****
		builder.RegisterType<UserService>().As<IUserService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<MenuService>().As<IMenuService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<PageService>().As<IPageService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<HomeSectionService>().As<IHomeSectionService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<DesignService>().As<IDesignService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

        builder.RegisterType<DetailSectionService>().As<IDetailSectionService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

        builder.RegisterType<SolutionGroupService>().As<ISolutionGroupService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<SubMenuService>().As<ISubMenuService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<ProductGroupService>().As<IProductGroupService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<ProductService>().As<IProductService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<ProjectService>().As<IProjectService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<ReferanceGroupService>().As<IReferanceGroupService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<ReferanceService>().As<IReferanceService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<BlogService>().As<IBlogService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<ContactMessageService>().As<IContactMessageService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<LinkService>().As<ILinkService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<AnnouncementService>().As<IAnnouncementService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<CompanyService>().As<ICompanyService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

		builder.RegisterType<SolutionService>().As<ISolutionService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();

        builder.RegisterType<SmtpSettingsService>().As<ISmtpSettingsService>()
			.EnableInterfaceInterceptors()
			.InterceptedBy(typeof(ValidationInterceptor), typeof(ExceptionHandlerInterceptor), typeof(CacheRemoveInterceptor), typeof(CacheRemoveGroupInterceptor), typeof(CacheInterceptor))
			.InstancePerLifetimeScope();
    }
}
