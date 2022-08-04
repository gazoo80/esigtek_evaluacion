using AutoMapper;
using Core.Entidades;
using Core.Interfaces;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio productoRepositorio;
        private readonly IMapper mapper;

        public ProductoServicio(IProductoRepositorio _productoRepositorio, IMapper _mapper)
        {
            productoRepositorio = _productoRepositorio;
            mapper = _mapper;
        }

        public async Task<List<ProductoViewModel>> ObtenerTodos()
        {
            var productos = await productoRepositorio.ObtenerTodos();
            var productosVM = mapper.Map<List<ProductoViewModel>>(productos);
            return productosVM;
        }

        public async Task<ProductoViewModel> ObtenerPorId(int id)
        {
            var producto = await productoRepositorio.ObtenerPorId(id);
            var productoVM = mapper.Map<ProductoViewModel>(producto);
            return productoVM;
        }

        public async Task<int?> Insertar(ProductoViewModel productoVM)
        {
            var producto = mapper.Map<Producto>(productoVM);
            var newId = await productoRepositorio.Insertar(producto);
            return newId;
        }

        public async Task<bool> Actualizar(ProductoViewModel productoVM)
        {
            var producto = mapper.Map<Producto>(productoVM);
            var res = await productoRepositorio.Actualizar(producto);
            return res;
        }

        public async Task<bool> Eliminar(int id)
        {
            var res = await productoRepositorio.Eliminar(id);
            return res;
        }            
    }
}
