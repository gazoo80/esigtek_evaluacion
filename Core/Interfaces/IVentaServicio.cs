using Core.Entidades;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IVentaServicio
    {
        Task<bool> Insert(VentaViewModel ventaVM);
    }
}
