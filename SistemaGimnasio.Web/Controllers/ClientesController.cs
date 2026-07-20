using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Web.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly IClienteService _service;
        private readonly IEntrenadorService _entrenadorService;

        public ClientesController(IClienteService service, IEntrenadorService entrenadorService)
        {
            _service = service;
            _entrenadorService = entrenadorService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerTodosAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _service.ObtenerPorIdAsync(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        public async Task<IActionResult> Create()
        {
            await CargarEntrenadoresAsync();
            return View(new Cliente
            {
                Activo = true,
                FechaRegistro = DateTime.Today,
                FechaNacimiento = DateTime.Today.AddYears(-18)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Cedula,Nombres,Apellidos,Telefono,Correo,FechaNacimiento,FechaRegistro,Activo,EntrenadorId")] Cliente cliente)
        {
            if (cliente.FechaNacimiento > DateTime.Today)
                ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento no puede ser futura.");

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertarAsync(cliente);
                    TempData["Success"] = "El cliente fue registrado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("Cedula", "Ya existe un cliente con esta cédula.");
                }
            }

            await CargarEntrenadoresAsync(cliente.EntrenadorId);
            return View(cliente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _service.ObtenerPorIdAsync(id.Value);
            if (cliente == null) return NotFound();
            await CargarEntrenadoresAsync(cliente.EntrenadorId);
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Cedula,Nombres,Apellidos,Telefono,Correo,FechaNacimiento,FechaRegistro,Activo,EntrenadorId")] Cliente cliente)
        {
            if (id != cliente.ClienteId) return NotFound();
            if (cliente.FechaNacimiento > DateTime.Today)
                ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento no puede ser futura.");

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.ActualizarAsync(cliente);
                    TempData["Success"] = "El cliente fue actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("Cedula", "Ya existe un cliente con esta cédula.");
                }
            }

            await CargarEntrenadoresAsync(cliente.EntrenadorId);
            return View(cliente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _service.ObtenerPorIdAsync(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                TempData["Success"] = "El cliente fue eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar el cliente porque tiene membresías o asistencias registradas.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarEntrenadoresAsync(object? seleccionado = null)
        {
            var entrenadores = await _entrenadorService.ObtenerTodosAsync();
            ViewData["EntrenadorId"] = new SelectList(entrenadores.Where(e => e.Activo), "EntrenadorId", "NombreCompleto", seleccionado);
        }
    }
}
