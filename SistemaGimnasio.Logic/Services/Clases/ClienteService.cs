using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes
                .Include(c => c.Entrenador)
                .OrderBy(c => c.Nombres)
                .ThenBy(c => c.Apellidos)
                .ToListAsync();
        }

        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Entrenador)
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task InsertarAsync(Cliente entidad)
        {
            entidad.FechaRegistro = entidad.FechaRegistro == default ? DateTime.Today : entidad.FechaRegistro;
            _context.Clientes.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Cliente entidad)
        {
            _context.Clientes.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.Clientes.FindAsync(id);
            if (entidad != null)
            {
                _context.Clientes.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
