using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class TipoMembresiaService : ITipoMembresiaService
    {
        private readonly ApplicationDbContext _context;

        public TipoMembresiaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoMembresia>> ObtenerTodosAsync()
        {
            return await _context.TiposMembresia
                .OrderBy(t => t.Nombre)
                .ToListAsync();
        }

        public async Task<TipoMembresia?> ObtenerPorIdAsync(int id)
        {
            return await _context.TiposMembresia
                .Include(t => t.Membresias)
                .FirstOrDefaultAsync(t => t.TipoMembresiaId == id);
        }

        public async Task InsertarAsync(TipoMembresia entidad)
        {
            _context.TiposMembresia.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(TipoMembresia entidad)
        {
            _context.TiposMembresia.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.TiposMembresia.FindAsync(id);
            if (entidad != null)
            {
                _context.TiposMembresia.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
