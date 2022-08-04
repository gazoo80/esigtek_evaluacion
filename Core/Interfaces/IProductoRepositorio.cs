using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ObtenerTodos();

        Task<Producto> ObtenerPorId(int id);

        Task<int?> Insertar(Producto producto);

        Task<bool> Actualizar(Producto producto);

        Task<bool> Eliminar(int id);
    }
}
