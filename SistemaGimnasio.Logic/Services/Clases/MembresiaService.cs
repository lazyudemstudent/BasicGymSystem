using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class MembresiaService : IMembresiaService
    {
        private readonly ApplicationDbContext _context;

        public MembresiaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Membresia>> ObtenerTodosAsync()
        {
            return await _context.Membresias
                .Include(m => m.Cliente)
                .Include(m => m.TipoMembresia)
                .OrderByDescending(m => m.FechaInicio)
                .ToListAsync();
        }

        public async Task<Membresia?> ObtenerPorIdAsync(int id)
        {
            return await _context.Membresias
                .Include(m => m.Cliente)
                .Include(m => m.TipoMembresia)
                .Include(m => m.Pagos)
                .FirstOrDefaultAsync(m => m.MembresiaId == id);
        }

        public async Task InsertarAsync(Membresia entidad)
        {
            var tipo = await _context.TiposMembresia.FindAsync(entidad.TipoMembresiaId)
                ?? throw new InvalidOperationException("El tipo de membresía seleccionado no existe.");

            entidad.FechaFin = entidad.FechaInicio.AddDays(tipo.DuracionDias);
            entidad.PrecioAcordado = tipo.Precio;

            _context.Membresias.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Membresia entidad)
        {
            var registro = await _context.Membresias.FindAsync(entidad.MembresiaId)
                ?? throw new InvalidOperationException("La membresía no existe.");
            var tipo = await _context.TiposMembresia.FindAsync(entidad.TipoMembresiaId)
                ?? throw new InvalidOperationException("El tipo de membresía seleccionado no existe.");

            registro.ClienteId = entidad.ClienteId;
            registro.TipoMembresiaId = entidad.TipoMembresiaId;
            registro.FechaInicio = entidad.FechaInicio;
            registro.FechaFin = entidad.FechaInicio.AddDays(tipo.DuracionDias);
            registro.PrecioAcordado = tipo.Precio;
            registro.Activa = entidad.Activa;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.Membresias.FindAsync(id);
            if (entidad != null)
            {
                _context.Membresias.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
