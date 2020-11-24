using System;
using System.Collections.Generic;
using System.Text;

namespace ControlCSA.Models
{
    public class AgendaRis
    {
        public string RUT { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaAgendamiento { get; set; }
        public string EstadoAgenda { get; set; }
        public string CodigoEstudio { get; set; }
        public string DescripcionEstudio { get; set; }
    }
}
