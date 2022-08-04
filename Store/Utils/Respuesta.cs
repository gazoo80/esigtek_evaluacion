using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Utils
{
    public class Respuesta
    {
        public bool Ok { get; set; }
        public string Msg { get; set; }
        public string ToRedirect { get; set; }
    }
}
