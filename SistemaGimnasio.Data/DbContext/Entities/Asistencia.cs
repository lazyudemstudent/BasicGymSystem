#nullable disable
using System.ComponentModel.DataAnnotations;

namespace SistemaGimnasio.Data.DbContext.Entities
{
    public class Asistencia
    {
        public int AsistenciaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La fecha y hora de entrada es obligatoria")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de entrada")]
        public DateTime FechaHoraEntrada { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de salida")]
        public DateTime? FechaHoraSalida { get; set; }

        [StringLength(500, ErrorMessage = "La observación no puede exceder los 500 caracteres")]
        [Display(Name = "Observación")]
        public string Observacion { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
