using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }
        public void Initialize() 
        {
            if (!_roleManager.RoleExistsAsync(WebSiteRoles.WebSiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSiteAuthor)).GetAwaiter().GetResult(); 
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                }, "Admin@0011").Wait();
                var appUser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebSiteRoles.WebSiteAdmin).GetAwaiter().GetResult();
                }
                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                        Title = "About us",
                    Slug = "about"
                    },
                    new Page()
                    {
                    Title = "Contact us",
                    Slug = "contact"
                    },
                    new Page()
                    {
                    Title = "Privacy Policy",
                    Slug = "privacy"
                    }
                };

                _context.Pages.AddRange(listOfPages);
                _context.SaveChanges();
            }
        }
    }
}
