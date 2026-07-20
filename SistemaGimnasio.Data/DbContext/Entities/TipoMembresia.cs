#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SistemaGimnasio.Data.DbContext.Entities
{
    public class TipoMembresia
    {
        public int TipoMembresiaId { get; set; }

        [Required(ErrorMessage = "El nombre de la membresía es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre de la membresía")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Range(1, 3650, ErrorMessage = "La duración debe estar entre 1 y 3650 días")]
        [Display(Name = "Duración en días")]
        public int DuracionDias { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "El precio debe ser mayor que cero")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        public bool Activa { get; set; } = true;

        public virtual ICollection<Membresia> Membresias { get; set; } = new List<Membresia>();
    }
}
