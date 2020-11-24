using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlCSA.Models
{
    public class ReservaClini
    {
        [JsonProperty("RESE_ID")]
        public long ReseId { get; set; }

        [JsonProperty("PRES_ID")]
        public long PresId { get; set; }

        [JsonProperty("EXA_ID")]
        public object ExaId { get; set; }

        [JsonProperty("PRES_DESCRIPCION")]
        public string PresDescripcion { get; set; }

        [JsonProperty("ESP_DESCRIPCION")]
        public string EspDescripcion { get; set; }

        [JsonProperty("ERES_DESCRIPCION")]
        public string EresDescripcion { get; set; }

        [JsonProperty("RESE_FECHA_RESERVA")]
        public string ReseFechaReserva { get; set; }

        [JsonProperty("RESE_HORA_INI")]
        public DateTimeOffset ReseHoraIni { get; set; }

        [JsonProperty("RESE_HORA_FIN")]
        public DateTimeOffset ReseHoraFin { get; set; }

        [JsonProperty("USU_RUTA_IMAGEN")]
        public string UsuRutaImagen { get; set; }

        [JsonProperty("CLI_ID")]
        public string CliId { get; set; }

        [JsonProperty("CLI_DESCRIPCION")]
        public string CliDescripcion { get; set; }

        [JsonProperty("CLIS_ID")]
        public long ClisId { get; set; }

        [JsonProperty("CLIS_DESCRIPCION")]
        public string ClisDescripcion { get; set; }
    }
}
