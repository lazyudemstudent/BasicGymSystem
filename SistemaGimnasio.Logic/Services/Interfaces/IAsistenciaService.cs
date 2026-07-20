using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface IAsistenciaService
    {
        Task<IEnumerable<Asistencia>> ObtenerTodosAsync();
        Task<Asistencia?> ObtenerPorIdAsync(int id);
        Task InsertarAsync(Asistencia entidad);
        Task ActualizarAsync(Asistencia entidad);
        Task EliminarAsync(int id);
    }
}
