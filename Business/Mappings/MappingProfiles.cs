using AutoMapper;
using Model.Auth.SignUp;
using Model.Dtos.Announcement_;
using Model.Dtos.Blog_;
using Model.Dtos.Company_;
using Model.Dtos.ContactMessage_;
using Model.Dtos.Design_;
using Model.Dtos.DetailSection_;
using Model.Dtos.HomeSection_;
using Model.Dtos.Link_;
using Model.Dtos.Menu_;
using Model.Dtos.Page_;
using Model.Dtos.Product_;
using Model.Dtos.ProductGroup_;
using Model.Dtos.Project_;
using Model.Dtos.Referance_;
using Model.Dtos.ReferanceGroup_;
using Model.Dtos.Solution_;
using Model.Dtos.SolutionGroup_;
using Model.Dtos.SubMenu_;
using Model.Dtos.User_;
using Model.Entities;

namespace Business.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<source, dest>

        #region User
        CreateMap<User, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SignUpRequest, User>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();

        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();

        #endregion

        #region Menu
        CreateMap<Menu, Menu>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Menu, MenuDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dest => dest.SubMenuList,
                opt => opt.MapFrom(src => src.SubMenuList != default ?
                    src.SubMenuList
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<MenuCreateDto, Menu>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ReverseMap();

        CreateMap<MenuUpdateDto, Menu>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ReverseMap();

        #endregion

        #region Page
        CreateMap<Page, Page>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Page, PageDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DesignId, opt => opt.MapFrom(src => src.DesignId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForMember(dest => dest.ShowFooter, opt => opt.MapFrom(src => src.ShowFooter))
            .ForMember(
                dest => dest.DesignModel,
                opt => opt.MapFrom(src => src.Design != default ?
                    src.Design
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<PageCreateDto, Page>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForMember(dest => dest.ShowFooter, opt => opt.MapFrom(src => src.ShowFooter))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Design, opt => opt.MapFrom(src => src.DesignCreateModel))
            .ReverseMap();

        CreateMap<PageUpdateDto, Page>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForMember(dest => dest.ShowFooter, opt => opt.MapFrom(src => src.ShowFooter))
            .ReverseMap();

        #endregion

        #region HomeSection
        CreateMap<HomeSection, HomeSection>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<HomeSection, HomeSectionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DesignId, opt => opt.MapFrom(src => src.DesignId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(
                dest => dest.DesignModel,
                opt => opt.MapFrom(src => src.Design != default ?
                    src.Design
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<HomeSectionCreateDto, HomeSection>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Design, opt => opt.MapFrom(src => src.DesignCreateModel))
            .ReverseMap();

        CreateMap<HomeSectionUpdateDto, HomeSection>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        #endregion

        #region DetailSection
        CreateMap<DetailSection, DetailSection>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<DetailSection, DetailSectionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(
                dest => dest.Design,
                opt => opt.MapFrom(src => src.Design != default ?
                    src.Design
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ProductDetailSectionCreateDto, DetailSection>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Design, opt => opt.MapFrom(src => src.DesignCreateModel))
            .ReverseMap();

        CreateMap<ProductDetailSectionUpdateDto, DetailSection>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ReverseMap();

        #endregion

        #region Design
        CreateMap<Design, Design>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Design, DesignDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Html))
            .ForMember(dest => dest.Css, opt => opt.MapFrom(src => src.Css))
            .ForMember(dest => dest.Script, opt => opt.MapFrom(src => src.Script))
            .ForMember(dest => dest.ProjectJson, opt => opt.MapFrom(src => src.ProjectJson))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Design, DesignRenderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Html))
            .ForMember(dest => dest.Css, opt => opt.MapFrom(src => src.Css))
            .ForMember(dest => dest.Script, opt => opt.MapFrom(src => src.Script)) 
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));


        CreateMap<DesignCreateDto, Design>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Html))
            .ForMember(dest => dest.Css, opt => opt.MapFrom(src => src.Css))
            .ForMember(dest => dest.Script, opt => opt.MapFrom(src => src.Script))
            .ForMember(dest => dest.ProjectJson, opt => opt.MapFrom(src => src.ProjectJson))
            .ReverseMap();

        CreateMap<DesignUpdateDto, Design>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Html, opt => opt.MapFrom(src => src.Html))
            .ForMember(dest => dest.Css, opt => opt.MapFrom(src => src.Css))
            .ForMember(dest => dest.Script, opt => opt.MapFrom(src => src.Script))
            .ForMember(dest => dest.ProjectJson, opt => opt.MapFrom(src => src.ProjectJson))
            .ReverseMap();

        #endregion

        #region SolutionGroup
        CreateMap<SolutionGroup, SolutionGroup>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SolutionGroup, SolutionGroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SolutionGroup, SolutionGroupDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForMember(
                dest => dest.SolutionList,
                opt => opt.MapFrom(src => src.Solutions != default ?
                    src.Solutions
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SolutionGroupCreateDto, SolutionGroup>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ReverseMap();

        CreateMap<SolutionGroupUpdateDto, SolutionGroup>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ReverseMap();

        #endregion

        #region SubMenu
        CreateMap<SubMenu, SubMenu>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SubMenu, SubMenuDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SubMenuCreateDto, SubMenu>()
            .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ReverseMap();

        CreateMap<SubMenuUpdateDto, SubMenu>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ReverseMap();

        #endregion

        #region ProductGroup
        CreateMap<ProductGroup, ProductGroup>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ProductGroup, ProductGroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ProductGroup, ProductGroupDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ForMember(
                dest => dest.ProductList,
                opt => opt.MapFrom(src => src.Products != default ?
                    src.Products
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ProductGroupCreateDto, ProductGroup>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ReverseMap();

        CreateMap<ProductGroupUpdateDto, ProductGroup>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => src.PathName))
            .ReverseMap();

        #endregion

        #region Product
        CreateMap<Product, Product>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductGroupId, opt => opt.MapFrom(src => src.ProductGroupId))
            .ForMember(dest => dest.DesignId, opt => opt.MapFrom(src => src.DesignId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.FriendlyUrl, opt => opt.MapFrom(src => src.FriendlyUrl))
            .ForMember(
                dest => dest.Design,
                opt => opt.MapFrom(src => src.Design != default ?
                    src.Design
                    : default
                )
            )
            //.ForMember(
            //    dest => dest.DetailSections,
            //    opt => opt.MapFrom(src => src.DetailSections != default ?
            //        src.DetailSections
            //        : default
            //    )
            //)
            .ForMember(dest => dest.DetailSections, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ProductGroupId, opt => opt.MapFrom(src => src.ProductGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.FriendlyUrl, opt => opt.MapFrom(src => src.FriendlyUrl))
            .ForMember(dest => dest.Design, opt => opt.MapFrom(src => src.DesignCreateModel))
            .ReverseMap();

        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductGroupId, opt => opt.MapFrom(src => src.ProductGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.FriendlyUrl, opt => opt.MapFrom(src => src.FriendlyUrl))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ReverseMap();

        #endregion

        #region Project
        CreateMap<Project, Project>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
            .ForMember(dest => dest.DesignId, opt => opt.MapFrom(src => src.DesignId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) 
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(
                dest => dest.Design,
                opt => opt.MapFrom(src => src.Design != default ?
                    src.Design
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ProjectCreateDto, Project>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Design, opt => opt.MapFrom(src => src.DesignCreateModel))
            .ReverseMap();

        CreateMap<ProjectUpdateDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();

        #endregion

        #region ReferanceGroup
        CreateMap<ReferanceGroup, ReferanceGroup>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ReferanceGroup, ReferanceGroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ReferanceGroup, ReferanceGroupDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.ReferanceList,
                opt => opt.MapFrom(src => src.Referances != default ?
                    src.Referances
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ReferanceGroupCreateDto, ReferanceGroup>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<ReferanceGroupUpdateDto, ReferanceGroup>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        #endregion

        #region Referance
        CreateMap<Referance, Referance>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Referance, ReferanceDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ReferanceGroupId, opt => opt.MapFrom(src => src.ReferanceGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ReferanceCreateDto, Referance>()
            .ForMember(dest => dest.ReferanceGroupId, opt => opt.MapFrom(src => src.ReferanceGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();

        CreateMap<ReferanceUpdateDto, Referance>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ReferanceGroupId, opt => opt.MapFrom(src => src.ReferanceGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();

        #endregion

        #region Blog
        CreateMap<Blog, Blog>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Blog, BlogDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDateUtc.HasValue ? src.CreateDateUtc.Value.ToLocalTime() : default))
            .ForMember(
                dest => dest.Author,
                opt => opt.MapFrom(src => src.Author != default ?
                    src.Author
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<BlogCreateDto, Blog>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ReverseMap();

        CreateMap<BlogUpdateDto, Blog>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();

        #endregion

        #region ContactMessage
        CreateMap<ContactMessage, ContactMessage>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<ContactMessageCreateDto, ContactMessage>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.NormalizedEmail))
            .ForMember(dest => dest.ClientIp, opt => opt.MapFrom(src => src.ClientIp))
            .ForMember(dest => dest.SendingStatus, opt => opt.MapFrom(src => src.SendingStatus))
            .ReverseMap();

        #endregion

        #region Link
        CreateMap<Link, Link>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<LinkCreateDto, Link>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ReverseMap();

        CreateMap<LinkUpdateDto, Link>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ReverseMap();

        #endregion

        #region Announcement
        CreateMap<Announcement, Announcement>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<AnnouncementCreateDto, Announcement>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ReverseMap();

        CreateMap<AnnouncementUpdateDto, Announcement>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ReverseMap();

        #endregion

        #region Company
        CreateMap<Company, Company>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));
        
        CreateMap<Company, CompanyLicenseKeysDto>()
            .ForMember(dest => dest.GapesJSLicenseKey, opt => opt.MapFrom(src => src.GapesJSLicenseKey))
            .ForMember(dest => dest.CKEditorLicenseKey, opt => opt.MapFrom(src => src.CKEditorLicenseKey))
            .ReverseMap();

        CreateMap<CompanyCreateDto, Company>()
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => src.Since))
            .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.EmailAdresses, opt => opt.MapFrom(src => src.EmailAdresses))
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers))
            .ForMember(dest => dest.FaxNumbers, opt => opt.MapFrom(src => src.FaxNumbers))
            .ForMember(dest => dest.WorkingStartTime, opt => opt.MapFrom(src => src.WorkingStartTime))
            .ForMember(dest => dest.WorkingEndTime, opt => opt.MapFrom(src => src.WorkingEndTime))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.GapesJSLicenseKey, opt => opt.MapFrom(src => src.GapesJSLicenseKey))
            .ForMember(dest => dest.CKEditorLicenseKey, opt => opt.MapFrom(src => src.CKEditorLicenseKey))
            .ReverseMap();

        CreateMap<CompanyUpdateDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Since, opt => opt.MapFrom(src => src.Since))
            .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.EmailAdresses, opt => opt.MapFrom(src => src.EmailAdresses))
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers))
            .ForMember(dest => dest.FaxNumbers, opt => opt.MapFrom(src => src.FaxNumbers))
            .ForMember(dest => dest.WorkingStartTime, opt => opt.MapFrom(src => src.WorkingStartTime))
            .ForMember(dest => dest.WorkingEndTime, opt => opt.MapFrom(src => src.WorkingEndTime))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.GapesJSLicenseKey, opt => opt.MapFrom(src => src.GapesJSLicenseKey))
            .ForMember(dest => dest.CKEditorLicenseKey, opt => opt.MapFrom(src => src.CKEditorLicenseKey))
            .ReverseMap();

        #endregion

        #region Solution
        CreateMap<Solution, Solution>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<Solution, SolutionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SolutionGroupId, opt => opt.MapFrom(src => src.SolutionGroupId))
            .ForMember(dest => dest.DesignId, opt => opt.MapFrom(src => src.DesignId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FriendlyUrl, opt => opt.MapFrom(src => src.FriendlyUrl))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(
                dest => dest.Design,
                opt => opt.MapFrom(src => src.Design != default ?
                    src.Design
                    : default
                )
            )
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));

        CreateMap<SolutionCreateDto, Solution>()
            .ForMember(dest => dest.SolutionGroupId, opt => opt.MapFrom(src => src.SolutionGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FriendlyUrl, opt => opt.MapFrom(src => src.FriendlyUrl))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Design, opt => opt.MapFrom(src => src.DesignCreateModel))
            .ReverseMap();

        CreateMap<SolutionUpdateDto, Solution>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SolutionGroupId, opt => opt.MapFrom(src => src.SolutionGroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.FriendlyUrl, opt => opt.MapFrom(src => src.FriendlyUrl))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ReverseMap();

        #endregion

        #region SmtpSettings
        CreateMap<SmtpSettings, SmtpSettings>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => !Equals(srcMember, destMember)));
        #endregion
    }
}
