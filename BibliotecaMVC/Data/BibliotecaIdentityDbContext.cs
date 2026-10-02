using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Data
{
    public class BibliotecaIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public BibliotecaIdentityDbContext(DbContextOptions<BibliotecaIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
