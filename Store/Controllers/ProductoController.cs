using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoServicio productoServicio;

        public ProductoController(IProductoServicio _productoServicio)
        {
            productoServicio = _productoServicio;
        }


        // GET: ProductoController
        public async Task<ActionResult> Index()
        {
            var productos = await productoServicio.ObtenerTodos();  
            return View(productos);
        }

        // GET: ProductoController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var producto = await productoServicio.ObtenerPorId(id.Value);

            if (producto == null)
            {
                return NotFound();
            }
            
            return View(producto);
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductoViewModel producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(producto);
                }

                int? id = await productoServicio.Insertar(producto);

                if (id == null)
                {
                    throw new Exception();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar el producto - " + ex.Message);
                return View(producto);
            }
        }

        // GET: ProductoController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var producto = await productoServicio.ObtenerPorId(id.Value);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductoViewModel producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(producto);
                }

                var result = await productoServicio.Actualizar(producto);

                if (!result)
                {
                    throw new Exception();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el producto - " + ex.Message);
                return View(producto);
            }
        }

        // GET: ProductoController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var producto = await productoServicio.ObtenerPorId(id.Value);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            try
            {
                var result = await productoServicio.Eliminar(id);

                if (!result) throw new Exception();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el producto - " + ex.Message);
                return View("Delete");
            }
        }
    }
}
