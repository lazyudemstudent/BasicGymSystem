#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SistemaGimnasio.Data.DbContext.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }

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

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [StringLength(150, ErrorMessage = "El correo no puede exceder los 150 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Today;

        public bool Activo { get; set; } = true;

        [Display(Name = "Entrenador asignado")]
        public int? EntrenadorId { get; set; }

        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public virtual Entrenador Entrenador { get; set; }
        public virtual ICollection<Membresia> Membresias { get; set; } = new List<Membresia>();
        public virtual ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}
