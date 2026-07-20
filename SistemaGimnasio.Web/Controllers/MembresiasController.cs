using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Web.Controllers
{
    [Authorize]
    public class MembresiasController : Controller
    {
        private readonly IMembresiaService _service;
        private readonly IClienteService _clienteService;
        private readonly ITipoMembresiaService _tipoService;

        public MembresiasController(
            IMembresiaService service,
            IClienteService clienteService,
            ITipoMembresiaService tipoService)
        {
            _service = service;
            _clienteService = clienteService;
            _tipoService = tipoService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerTodosAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var membresia = await _service.ObtenerPorIdAsync(id.Value);
            if (membresia == null) return NotFound();
            return View(membresia);
        }

        public async Task<IActionResult> Create()
        {
            await CargarListasAsync();
            return View(new Membresia { FechaInicio = DateTime.Today, Activa = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembresiaId,ClienteId,TipoMembresiaId,FechaInicio,Activa")] Membresia membresia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertarAsync(membresia);
                    TempData["Success"] = "La membresía fue asignada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            await CargarListasAsync(membresia.ClienteId, membresia.TipoMembresiaId);
            return View(membresia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var membresia = await _service.ObtenerPorIdAsync(id.Value);
            if (membresia == null) return NotFound();
            await CargarListasAsync(membresia.ClienteId, membresia.TipoMembresiaId);
            return View(membresia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembresiaId,ClienteId,TipoMembresiaId,FechaInicio,Activa")] Membresia membresia)
        {
            if (id != membresia.MembresiaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.ActualizarAsync(membresia);
                    TempData["Success"] = "La membresía fue actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            await CargarListasAsync(membresia.ClienteId, membresia.TipoMembresiaId);
            return View(membresia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var membresia = await _service.ObtenerPorIdAsync(id.Value);
            if (membresia == null) return NotFound();
            return View(membresia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                TempData["Success"] = "La membresía fue eliminada correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar la membresía porque tiene pagos registrados.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarListasAsync(object? clienteSeleccionado = null, object? tipoSeleccionado = null)
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            var tipos = await _tipoService.ObtenerTodosAsync();
            ViewData["ClienteId"] = new SelectList(clientes.Where(c => c.Activo), "ClienteId", "NombreCompleto", clienteSeleccionado);
            ViewData["TipoMembresiaId"] = new SelectList(tipos.Where(t => t.Activa), "TipoMembresiaId", "Nombre", tipoSeleccionado);
        }
    }
}
