using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMVC.Models
{
    public class Libro
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(100)]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(80)]
        public string Categoria { get; set; } = string.Empty;

        [Range(0.01, 100000, ErrorMessage = "El precio debe ser mayor que cero.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }

        [Display(Name = "Nombre de la imagen")]
        [StringLength(150)]
        public string Imagen { get; set; } = string.Empty;
    }
}
