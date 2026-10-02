using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario o correo es obligatorio.")]
        [Display(Name = "Usuario o correo electrónico")]
        public string UsuarioOCorreo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;

        [Display(Name = "Recordar sesión")]
        public bool Recordarme { get; set; }
    }
}
