using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Web.Controllers
{
    [Authorize]
    public class TiposMembresiaController : Controller
    {
        private readonly ITipoMembresiaService _service;

        public TiposMembresiaController(ITipoMembresiaService service)
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
            var tipo = await _service.ObtenerPorIdAsync(id.Value);
            if (tipo == null) return NotFound();
            return View(tipo);
        }

        public IActionResult Create()
        {
            return View(new TipoMembresia { Activa = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoMembresiaId,Nombre,Descripcion,DuracionDias,Precio,Activa")] TipoMembresia tipo)
        {
            if (ModelState.IsValid)
            {
                await _service.InsertarAsync(tipo);
                TempData["Success"] = "El tipo de membresía fue registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var tipo = await _service.ObtenerPorIdAsync(id.Value);
            if (tipo == null) return NotFound();
            return View(tipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoMembresiaId,Nombre,Descripcion,DuracionDias,Precio,Activa")] TipoMembresia tipo)
        {
            if (id != tipo.TipoMembresiaId) return NotFound();

            if (ModelState.IsValid)
            {
                await _service.ActualizarAsync(tipo);
                TempData["Success"] = "El tipo de membresía fue actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var tipo = await _service.ObtenerPorIdAsync(id.Value);
            if (tipo == null) return NotFound();
            return View(tipo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                TempData["Success"] = "El tipo de membresía fue eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar el tipo porque está asociado a una membresía.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
