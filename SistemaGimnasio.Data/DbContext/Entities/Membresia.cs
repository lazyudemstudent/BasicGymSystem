#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SistemaGimnasio.Data.DbContext.Entities
{
    public class Membresia
    {
        public int MembresiaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de membresía")]
        [Display(Name = "Tipo de membresía")]
        public int TipoMembresiaId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de finalización")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Precio acordado")]
        public decimal PrecioAcordado { get; set; }

        public bool Activa { get; set; } = true;

        public string DescripcionCompleta => $"{Cliente?.NombreCompleto} - {TipoMembresia?.Nombre}";

        public virtual Cliente Cliente { get; set; }
        public virtual TipoMembresia TipoMembresia { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
