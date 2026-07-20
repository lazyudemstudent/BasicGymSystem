#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SistemaGimnasio.Data.DbContext.Entities
{
    public class Pago
    {
        public int PagoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una membresía")]
        [Display(Name = "Membresía")]
        public int MembresiaId { get; set; }

        [Required(ErrorMessage = "La fecha de pago es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de pago")]
        public DateTime FechaPago { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "El monto debe ser mayor que cero")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [StringLength(50, ErrorMessage = "El método de pago no puede exceder los 50 caracteres")]
        [Display(Name = "Método de pago")]
        public string MetodoPago { get; set; }

        [StringLength(100, ErrorMessage = "La referencia no puede exceder los 100 caracteres")]
        public string Referencia { get; set; }

        [StringLength(500, ErrorMessage = "La observación no puede exceder los 500 caracteres")]
        [Display(Name = "Observación")]
        public string Observacion { get; set; }

        public virtual Membresia Membresia { get; set; }
    }
}
