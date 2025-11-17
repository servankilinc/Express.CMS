using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models.UI;

namespace WebUI.Areas.Admin.ViewComponents;

public class SideMenuViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var menuItems = new List<MenuItem>()
        {
            new MenuItem
            {
                Title = "Ana sayfa",
                Icon = "fa-brands fa-magento",
                Path = "/Admin/Home/Index",
                Type = 1,
            },
            new MenuItem
            {
                Title = "Kullanıcılar", 
                Icon = "fa-solid fa-users",
                Path = "/Admin/User/Index",
                Type = 1,
            },
            new MenuItem
            {
                Title = "Çözümlerimiz",
                Icon = "fa-solid fa-sitemap",
                Type = 0,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Title = "Çözümler",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/Solution/Index",
                        Type = 1,
                    },
                    new MenuItem
                    {
                        Title = "Çözüm Grupları",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/SolutionGroup/Index",
                        Type = 1,
                    },
                }
            },
            new MenuItem
            {
                Title = "Ürünlerimiz",
                Icon = "fa-regular fa-paste",
                Type = 0,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Title = "Ürünler",
                        Icon = "fa-solid fa-file-circle-plus",
                        Path = "/Admin/Product/Index",
                        Type = 1,
                    },
                    new MenuItem
                    {
                        Title = "Ürün Grupları",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/ProductGroup/Index",
                        Type = 1,
                    }
                }
            },
            new MenuItem
            {
                Title = "Referanslarımız",
                Icon = "fa-solid fa-earth-europe",
                Type = 0,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Title = "Referanslar",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/Referance/Index",
                        Type = 1,
                    },
                    new MenuItem
                    {
                        Title = "Referans Grupları",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/ReferanceGroup/Index",
                        Type = 1,
                    }
                }
            },
            new MenuItem
            {
                Title = "Projeler",
                Icon = "fa-solid fa-person-chalkboard",
                Path = "/Admin/Project/Index",
                Type = 1, 
            },
            new MenuItem
            {
                Title = "İletişim Mesajları",
                Icon = "fa-regular fa-comments",
                Path = "/Admin/ContactMessage/Index",
                Type = 1
            },
            new MenuItem
            {
                Title = "Menu",
                Icon = "fa-solid fa-bars",
                Type = 0,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Title = "Menuler",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/Menu/Index",
                        Type = 1,
                    },
                    new MenuItem
                    {
                        Title = "Alt Menuler",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/SubMenu/Index",
                        Type = 1,
                    },
                }
            },
            new MenuItem
            {
                Title = "Blog",
                Icon = "fa-solid fa-newspaper",
                Type = 0,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Title = "Bloglar",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/Blog/Index",
                        Type = 1,
                    },
                    new MenuItem
                    {
                        Title = "Blog Oluştur",
                        Icon = "fa-solid fa-file-circle-plus",
                        Path = "/Admin/Blog/Create",
                        Type = 1,
                    }
                }
            },
            new MenuItem
            {
                Title = "Ana sayfa Yönetimi",
                Icon = "fa-solid fa-house-laptop",
                Type = 0,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem
                    {
                        Title = "Bölümler",
                        Icon = "fa-regular fa-file-lines",
                        Path = "/Admin/HomeSection/Index",
                        Type = 1,
                    },
                    new MenuItem
                    {
                        Title = "Bölüm Oluştur",
                        Icon = "fa-solid fa-file-circle-plus",
                        Path = "/Admin/HomeSection/Create",
                        Type = 1,
                    }
                }
            },
            new MenuItem
            {
                Title = "Bağlantılar",
                Icon = "fa-solid fa-link",
                Path = "/Admin/Link/Index",
                Type = 1,
            },
            new MenuItem
            {
                Title = "Firma Bilgileri",
                Icon = "fa-solid fa-sliders",
                Path = "/Admin/Company/Index",
                Type = 1,
            },
            new MenuItem
            {
                Title = "Duyurular",
                Icon = "fa-solid fa-bullhorn",
                Path = "/Admin/Announcement/Index",
                Type = 1,
            },
            new MenuItem
            {
                Title = "Sayfalar",
                Icon = "fa-solid fa-sheet-plastic",
                Path = "/Admin/Page/Index",
                Type = 1
            },
            new MenuItem
            {
                Title = "Smtp Ayarları",
                Icon = "fa-solid fa-envelope-open-text",
                Path = "/Admin/SmtpSettings/Index",
                Type = 1
            }
        };
        return View(menuItems);
    }
}