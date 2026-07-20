using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface IMembresiaService
    {
        Task<IEnumerable<Membresia>> ObtenerTodosAsync();
        Task<Membresia?> ObtenerPorIdAsync(int id);
        Task InsertarAsync(Membresia entidad);
        Task ActualizarAsync(Membresia entidad);
        Task EliminarAsync(int id);
    }
}
