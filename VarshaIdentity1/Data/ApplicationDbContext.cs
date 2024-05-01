using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace VarshaIdentity1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        IHttpContextAccessor _contextAccessor { get; set; }

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            modelBuilder.Entity<IdentityRole>().HasData(admin, user);
        }
        private void OnBeforeSaving()
        {
            var user = GetUser();
            var currentUsername = !string.IsNullOrEmpty(user) ? user : "varsha-Identity";
        }
        private string GetUser()
        {
            try
            {
                var user = _contextAccessor.HttpContext.User;
                return user.FindFirstValue(ClaimTypes.Email);
            }
            catch (Exception)
            {
                Console.WriteLine("Exception found while finding user in DbContext!");
                return null;
            }
        }
    }
}
