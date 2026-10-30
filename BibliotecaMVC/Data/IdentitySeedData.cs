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

            foreach (var nombreRol in new[] { RolesBiblioteca.Admin, RolesBiblioteca.Bibliotecario, RolesBiblioteca.Usuario })
            {
                if (!await roleManager.RoleExistsAsync(nombreRol))
                {
                    await roleManager.CreateAsync(new IdentityRole(nombreRol));
                }
            }

            await CrearUsuarioConRolAsync(userManager, "admin", "admin@biblioteca.com", "Admin123", RolesBiblioteca.Admin);
            await CrearUsuarioConRolAsync(userManager, "bibliotecario", "bibliotecario@biblioteca.com", "Biblio123", RolesBiblioteca.Bibliotecario);
            await CrearUsuarioConRolAsync(userManager, "usuario", "usuario@biblioteca.com", "Usuario123", RolesBiblioteca.Usuario);

            var usuarios = await userManager.Users.ToListAsync();
            foreach (var usuario in usuarios)
            {
                // Compatibilidad con los roles usados en la actividad anterior.
                if (await userManager.IsInRoleAsync(usuario, "Administrador"))
                {
                    await userManager.AddToRoleAsync(usuario, RolesBiblioteca.Admin);
                }

                if (await userManager.IsInRoleAsync(usuario, "Lector"))
                {
                    await userManager.AddToRoleAsync(usuario, RolesBiblioteca.Usuario);
                }

                var roles = await userManager.GetRolesAsync(usuario);
                if (!roles.Any(rol => rol == RolesBiblioteca.Admin || rol == RolesBiblioteca.Bibliotecario || rol == RolesBiblioteca.Usuario))
                {
                    await userManager.AddToRoleAsync(usuario, RolesBiblioteca.Usuario);
                }
            }
        }

        private static async Task CrearUsuarioConRolAsync(
            UserManager<IdentityUser> userManager,
            string nombreUsuario,
            string correo,
            string contrasena,
            string rol)
        {
            var usuario = await userManager.FindByEmailAsync(correo);
            if (usuario == null)
            {
                usuario = new IdentityUser
                {
                    UserName = nombreUsuario,
                    Email = correo
                };

                var resultado = await userManager.CreateAsync(usuario, contrasena);
                if (!resultado.Succeeded)
                {
                    return;
                }
            }

            if (!await userManager.IsInRoleAsync(usuario, rol))
            {
                await userManager.AddToRoleAsync(usuario, rol);
            }
        }
    }
}
