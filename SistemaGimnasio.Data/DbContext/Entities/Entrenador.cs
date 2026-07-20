#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SistemaGimnasio.Data.DbContext.Entities
{
    public class Entrenador
    {
        public int EntrenadorId { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(20, ErrorMessage = "La cédula no puede exceder los 20 caracteres")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100, ErrorMessage = "Los nombres no pueden exceder los 100 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder los 100 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria")]
        [StringLength(120, ErrorMessage = "La especialidad no puede exceder los 120 caracteres")]
        public string Especialidad { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [StringLength(150, ErrorMessage = "El correo no puede exceder los 150 caracteres")]
        public string Correo { get; set; }

        public bool Activo { get; set; } = true;

        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
