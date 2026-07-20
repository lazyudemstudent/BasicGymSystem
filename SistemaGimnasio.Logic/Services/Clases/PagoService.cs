using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class PagoService : IPagoService
    {
        private readonly ApplicationDbContext _context;

        public PagoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> ObtenerTodosAsync()
        {
            return await _context.Pagos
                .Include(p => p.Membresia)
                    .ThenInclude(m => m.Cliente)
                .Include(p => p.Membresia)
                    .ThenInclude(m => m.TipoMembresia)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
        }

        public async Task<Pago?> ObtenerPorIdAsync(int id)
        {
            return await _context.Pagos
                .Include(p => p.Membresia)
                    .ThenInclude(m => m.Cliente)
                .Include(p => p.Membresia)
                    .ThenInclude(m => m.TipoMembresia)
                .FirstOrDefaultAsync(p => p.PagoId == id);
        }

        public async Task InsertarAsync(Pago entidad)
        {
            _context.Pagos.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Pago entidad)
        {
            _context.Pagos.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.Pagos.FindAsync(id);
            if (entidad != null)
            {
                _context.Pagos.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
