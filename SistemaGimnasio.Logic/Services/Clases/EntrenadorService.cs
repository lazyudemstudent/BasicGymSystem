using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class EntrenadorService : IEntrenadorService
    {
        private readonly ApplicationDbContext _context;

        public EntrenadorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entrenador>> ObtenerTodosAsync()
        {
            return await _context.Entrenadores
                .OrderBy(e => e.Nombres)
                .ThenBy(e => e.Apellidos)
                .ToListAsync();
        }

        public async Task<Entrenador?> ObtenerPorIdAsync(int id)
        {
            return await _context.Entrenadores
                .Include(e => e.Clientes)
                .FirstOrDefaultAsync(e => e.EntrenadorId == id);
        }

        public async Task InsertarAsync(Entrenador entidad)
        {
            _context.Entrenadores.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Entrenador entidad)
        {
            _context.Entrenadores.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.Entrenadores.FindAsync(id);
            if (entidad != null)
            {
                _context.Entrenadores.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
