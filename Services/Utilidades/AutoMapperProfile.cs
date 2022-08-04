using AutoMapper;
using Core.Entidades;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Producto, ProductoViewModel>().ReverseMap();
            CreateMap<VentaViewModel, Venta>().ReverseMap();
            CreateMap<VentaDetalleViewModel, VentaDetalle>().ReverseMap();
        }
    }
}
