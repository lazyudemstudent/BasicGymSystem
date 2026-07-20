using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task InsertarAsync(Cliente entidad);
        Task ActualizarAsync(Cliente entidad);
        Task EliminarAsync(int id);
    }
}
