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
    public class VentaServicio : IVentaServicio
    {
        private readonly IVentaRepositorio ventaRepositorio;
        private readonly IMapper mapper;

        public VentaServicio(IVentaRepositorio _ventaRepositorio, IMapper _mapper)
        {
            ventaRepositorio = _ventaRepositorio;
            mapper = _mapper;
        }

        public async Task<bool> Insert(VentaViewModel ventaVM)
        {
            decimal total = 0;

            foreach (var detalle in ventaVM.Detalle)
            {
                total += detalle.Total;
            }

            ventaVM.Total = total;

            var venta = mapper.Map<Venta>(ventaVM);
            var res = await ventaRepositorio.Insertar(venta);
            return res;
        }
    }
}
