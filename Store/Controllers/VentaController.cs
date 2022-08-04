using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.ViewModels;
using Store.Utils;

namespace Store.Controllers
{
    public class VentaController : Controller
    {
        private readonly IVentaServicio ventaServicio;
        private readonly IProductoServicio productoServicio;

        public VentaController(IVentaServicio _ventaServicio, IProductoServicio _productoServicio)
        {
            ventaServicio = _ventaServicio;
            productoServicio = _productoServicio;
        }

        // GET: VentaController/Create
        public ActionResult Create()
        {
            var ventaView = new VentaViewModel();
            ventaView.Detalle = new List<VentaDetalleViewModel>();
            return View(ventaView);
        }

        // POST: VentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VentaViewModel ventaVM)
        {
            try
            {
                var res = await ventaServicio.Insert(ventaVM);

                if (!res)
                {
                    throw new Exception();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar la venta - " + ex.Message);
                return View(ventaVM);
            }
        }

        [HttpGet]
        public async Task<ActionResult> AddProducto()
        {
            var productos = await productoServicio.ObtenerTodos();
            ViewBag.ProductoId = new SelectList(productos, "Id", "Descripcion");
            return PartialView("_AgregarProducto");
        }

        [HttpGet]
        public async Task<ActionResult> EditProducto()
        {
            var productos = await productoServicio.ObtenerTodos();
            ViewBag.ProductoId = new SelectList(productos, "Id", "Descripcion");
            return PartialView("_EditarProducto");
        }
    }
}
