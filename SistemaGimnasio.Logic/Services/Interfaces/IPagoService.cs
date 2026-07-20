using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<Pago>> ObtenerTodosAsync();
        Task<Pago?> ObtenerPorIdAsync(int id);
        Task InsertarAsync(Pago entidad);
        Task ActualizarAsync(Pago entidad);
        Task EliminarAsync(int id);
    }
}
