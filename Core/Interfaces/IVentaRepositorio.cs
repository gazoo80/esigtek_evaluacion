using Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVentaRepositorio
    {
        Task<bool> Insertar(Venta venta);
    }
}
