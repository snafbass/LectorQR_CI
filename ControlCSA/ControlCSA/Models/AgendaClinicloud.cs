using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlCSA.Models
{
    public class AgendaClinicloud
    {
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reservas")]
        public List<ReservaClini> Reservas { get; set; }
    }
    
}
