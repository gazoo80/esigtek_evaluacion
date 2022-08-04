using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ViewModels
{
    public class VentaDetalleViewModel
    {
        public int Id { get; set; }
        public int VentaId { get; set; }

        [Required(ErrorMessage = "La selección de un producto es requerida")]
        public int ProductoId { get; set; }

        public string ProductoDescripcion { get; set; }

        [Required(ErrorMessage = "La cantidad de items es requerida")]
        public int Cantidad { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El precio es requerido")]
        public decimal Precio { get; set; }

        public decimal Total { get; set; }
    }
}
