using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface ITipoMembresiaService
    {
        Task<IEnumerable<TipoMembresia>> ObtenerTodosAsync();
        Task<TipoMembresia?> ObtenerPorIdAsync(int id);
        Task InsertarAsync(TipoMembresia entidad);
        Task ActualizarAsync(TipoMembresia entidad);
        Task EliminarAsync(int id);
    }
}
