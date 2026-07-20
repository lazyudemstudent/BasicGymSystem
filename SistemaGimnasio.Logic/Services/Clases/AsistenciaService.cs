using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Logic.Services.Clases
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asistencia>> ObtenerTodosAsync()
        {
            return await _context.Asistencias
                .Include(a => a.Cliente)
                .OrderByDescending(a => a.FechaHoraEntrada)
                .ToListAsync();
        }

        public async Task<Asistencia?> ObtenerPorIdAsync(int id)
        {
            return await _context.Asistencias
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(a => a.AsistenciaId == id);
        }

        public async Task InsertarAsync(Asistencia entidad)
        {
            if (entidad.FechaHoraSalida.HasValue && entidad.FechaHoraSalida < entidad.FechaHoraEntrada)
                throw new InvalidOperationException("La hora de salida no puede ser anterior a la hora de entrada.");

            _context.Asistencias.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Asistencia entidad)
        {
            if (entidad.FechaHoraSalida.HasValue && entidad.FechaHoraSalida < entidad.FechaHoraEntrada)
                throw new InvalidOperationException("La hora de salida no puede ser anterior a la hora de entrada.");

            _context.Asistencias.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.Asistencias.FindAsync(id);
            if (entidad != null)
            {
                _context.Asistencias.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
