using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ViewModels
{
    public class VentaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(5, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public string Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public List<VentaDetalleViewModel> Detalle { get; set; }
    }
}
