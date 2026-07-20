using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Web.Controllers
{
    [Authorize]
    public class EntrenadoresController : Controller
    {
        private readonly IEntrenadorService _service;

        public EntrenadoresController(IEntrenadorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerTodosAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var entrenador = await _service.ObtenerPorIdAsync(id.Value);
            if (entrenador == null) return NotFound();
            return View(entrenador);
        }

        public IActionResult Create()
        {
            return View(new Entrenador { Activo = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntrenadorId,Cedula,Nombres,Apellidos,Especialidad,Telefono,Correo,Activo")] Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.InsertarAsync(entrenador);
                    TempData["Success"] = "El entrenador fue registrado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("Cedula", "Ya existe un entrenador con esta cédula.");
                }
            }
            return View(entrenador);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var entrenador = await _service.ObtenerPorIdAsync(id.Value);
            if (entrenador == null) return NotFound();
            return View(entrenador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntrenadorId,Cedula,Nombres,Apellidos,Especialidad,Telefono,Correo,Activo")] Entrenador entrenador)
        {
            if (id != entrenador.EntrenadorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.ActualizarAsync(entrenador);
                    TempData["Success"] = "El entrenador fue actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("Cedula", "Ya existe un entrenador con esta cédula.");
                }
            }
            return View(entrenador);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var entrenador = await _service.ObtenerPorIdAsync(id.Value);
            if (entrenador == null) return NotFound();
            return View(entrenador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                TempData["Success"] = "El entrenador fue eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar el entrenador porque tiene clientes asignados.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
