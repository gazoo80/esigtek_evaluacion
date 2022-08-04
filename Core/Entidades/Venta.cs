using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entidades
{
    public class Venta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public List<VentaDetalle> Detalle { get; set; }
    }
}
