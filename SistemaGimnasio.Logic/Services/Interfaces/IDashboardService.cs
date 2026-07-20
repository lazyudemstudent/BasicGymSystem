using SistemaGimnasio.Models.Services;

namespace SistemaGimnasio.Logic.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<Dashboard_VM> ObtenerResumenAsync();
    }
}
