using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Data
{
    public static class IdentitySeedData
    {
        public static async Task InicializarAsync(IServiceProvider servicios)
        {
            var roleManager = servicios.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = servicios.GetRequiredService<UserManager<IdentityUser>>();

            foreach (var nombreRol in new[] { RolesBiblioteca.Administrador, RolesBiblioteca.Lector })
            {
                if (!await roleManager.RoleExistsAsync(nombreRol))
                {
                    await roleManager.CreateAsync(new IdentityRole(nombreRol));
                }
            }

            var administrador = await userManager.FindByEmailAsync("admin@biblioteca.com");
            if (administrador == null)
            {
                administrador = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@biblioteca.com"
                };

                var resultado = await userManager.CreateAsync(administrador, "Admin123");
                if (!resultado.Succeeded)
                {
                    administrador = null;
                }
            }

            if (administrador != null && !await userManager.IsInRoleAsync(administrador, RolesBiblioteca.Administrador))
            {
                await userManager.AddToRoleAsync(administrador, RolesBiblioteca.Administrador);
            }

            var usuarios = await userManager.Users.ToListAsync();
            foreach (var usuario in usuarios)
            {
                var roles = await userManager.GetRolesAsync(usuario);
                if (!roles.Any())
                {
                    await userManager.AddToRoleAsync(usuario, RolesBiblioteca.Lector);
                }
            }
        }
    }
}
