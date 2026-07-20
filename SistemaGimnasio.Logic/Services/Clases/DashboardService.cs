using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Logic.Services.Interfaces;
using SistemaGimnasio.Models.Services;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dashboard_VM> ObtenerResumenAsync()
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var primerDiaMes = new DateTime(hoy.Year, hoy.Month, 1);
            var primerDiaSiguienteMes = primerDiaMes.AddMonths(1);

            return new Dashboard_VM
            {
                TotalClientes = await _context.Clientes.CountAsync(),
                ClientesActivos = await _context.Clientes.CountAsync(c => c.Activo),
                EntrenadoresActivos = await _context.Entrenadores.CountAsync(e => e.Activo),
                MembresiasActivas = await _context.Membresias.CountAsync(m => m.Activa && m.FechaFin >= hoy),
                AsistenciasHoy = await _context.Asistencias.CountAsync(a => a.FechaHoraEntrada >= hoy && a.FechaHoraEntrada < manana),
                PagosMes = await _context.Pagos
                    .Where(p => p.FechaPago >= primerDiaMes && p.FechaPago < primerDiaSiguienteMes)
                    .SumAsync(p => (decimal?)p.Monto) ?? 0
            };
        }
    }
}
