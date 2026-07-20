using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Web.Controllers
{
    [Authorize]
    public class AsistenciasController : Controller
    {
        private readonly IAsistenciaService _service;
        private readonly IClienteService _clienteService;

        public AsistenciasController(IAsistenciaService service, IClienteService clienteService)
        {
            _service = service;
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerTodosAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var asistencia = await _service.ObtenerPorIdAsync(id.Value);
            if (asistencia == null) return NotFound();
            return View(asistencia);
        }

        public async Task<IActionResult> Create()
        {
            await CargarClientesAsync();
            return View(new Asistencia { FechaHoraEntrada = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsistenciaId,ClienteId,FechaHoraEntrada,FechaHoraSalida,Observacion")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertarAsync(asistencia);
                    TempData["Success"] = "La asistencia fue registrada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            await CargarClientesAsync(asistencia.ClienteId);
            return View(asistencia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var asistencia = await _service.ObtenerPorIdAsync(id.Value);
            if (asistencia == null) return NotFound();
            await CargarClientesAsync(asistencia.ClienteId);
            return View(asistencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsistenciaId,ClienteId,FechaHoraEntrada,FechaHoraSalida,Observacion")] Asistencia asistencia)
        {
            if (id != asistencia.AsistenciaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.ActualizarAsync(asistencia);
                    TempData["Success"] = "La asistencia fue actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            await CargarClientesAsync(asistencia.ClienteId);
            return View(asistencia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var asistencia = await _service.ObtenerPorIdAsync(id.Value);
            if (asistencia == null) return NotFound();
            return View(asistencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.EliminarAsync(id);
            TempData["Success"] = "La asistencia fue eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarClientesAsync(object? seleccionado = null)
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            ViewData["ClienteId"] = new SelectList(clientes.Where(c => c.Activo), "ClienteId", "NombreCompleto", seleccionado);
        }
    }
}
