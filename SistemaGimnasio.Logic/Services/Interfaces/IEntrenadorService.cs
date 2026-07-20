using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface IEntrenadorService
    {
        Task<IEnumerable<Entrenador>> ObtenerTodosAsync();
        Task<Entrenador?> ObtenerPorIdAsync(int id);
        Task InsertarAsync(Entrenador entidad);
        Task ActualizarAsync(Entrenador entidad);
        Task EliminarAsync(int id);
    }
}
