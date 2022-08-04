using Core.Entidades;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductoServicio
    {
        Task<List<ProductoViewModel>> ObtenerTodos();

        Task<ProductoViewModel> ObtenerPorId(int id);

        Task<int?> Insertar(ProductoViewModel productoVM);

        Task<bool> Actualizar(ProductoViewModel productoVM);

        Task<bool> Eliminar(int id);
    }
}
