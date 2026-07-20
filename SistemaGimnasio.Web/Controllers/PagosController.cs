using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaGimnasio.Data.DbContext.Entities;
using SistemaGimnasio.Logic.Services.Interfaces;

namespace SistemaGimnasio.Web.Controllers
{
    [Authorize]
    public class PagosController : Controller
    {
        private readonly IPagoService _service;
        private readonly IMembresiaService _membresiaService;

        public PagosController(IPagoService service, IMembresiaService membresiaService)
        {
            _service = service;
            _membresiaService = membresiaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerTodosAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _service.ObtenerPorIdAsync(id.Value);
            if (pago == null) return NotFound();
            return View(pago);
        }

        public async Task<IActionResult> Create()
        {
            await CargarMembresiasAsync();
            return View(new Pago { FechaPago = DateTime.Today, MetodoPago = "Efectivo" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagoId,MembresiaId,FechaPago,Monto,MetodoPago,Referencia,Observacion")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                await _service.InsertarAsync(pago);
                TempData["Success"] = "El pago fue registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            await CargarMembresiasAsync(pago.MembresiaId);
            return View(pago);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _service.ObtenerPorIdAsync(id.Value);
            if (pago == null) return NotFound();
            await CargarMembresiasAsync(pago.MembresiaId);
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagoId,MembresiaId,FechaPago,Monto,MetodoPago,Referencia,Observacion")] Pago pago)
        {
            if (id != pago.PagoId) return NotFound();

            if (ModelState.IsValid)
            {
                await _service.ActualizarAsync(pago);
                TempData["Success"] = "El pago fue actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            await CargarMembresiasAsync(pago.MembresiaId);
            return View(pago);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _service.ObtenerPorIdAsync(id.Value);
            if (pago == null) return NotFound();
            return View(pago);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.EliminarAsync(id);
            TempData["Success"] = "El pago fue eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarMembresiasAsync(object? seleccionado = null)
        {
            var membresias = await _membresiaService.ObtenerTodosAsync();
            ViewData["MembresiaId"] = new SelectList(membresias, "MembresiaId", "DescripcionCompleta", seleccionado);
        }
    }
}
