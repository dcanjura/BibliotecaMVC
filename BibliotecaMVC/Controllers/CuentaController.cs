using BibliotecaMVC.Data;
using BibliotecaMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CuentaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelo, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = await _userManager.FindByNameAsync(modelo.UsuarioOCorreo);
            usuario ??= await _userManager.FindByEmailAsync(modelo.UsuarioOCorreo);

            if (usuario != null)
            {
                var resultado = await _signInManager.PasswordSignInAsync(
                    usuario, modelo.Contrasena, modelo.Recordarme, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    TempData["Mensaje"] = "Sesión iniciada correctamente.";
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View(modelo);
        }

        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new IdentityUser
            {
                UserName = modelo.Usuario,
                Email = modelo.CorreoElectronico
            };

            var resultado = await _userManager.CreateAsync(usuario, modelo.Contrasena);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, RolesBiblioteca.Lector);
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                TempData["Mensaje"] = "Usuario registrado correctamente.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(modelo);
        }

        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Mensaje"] = "Sesión cerrada correctamente.";
            return RedirectToAction("Index", "Home");
        }
    }
}
