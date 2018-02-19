using System;
using Newtonsoft.Json;
using SQLite;

namespace Credigas.Models
{
    public class Visit
    {
        public Visit()
        {
        }

        #region Properties
        [PrimaryKey]
        [JsonProperty(PropertyName = "id_visita")]
        public long VisitId { get; set; }

        [JsonProperty(PropertyName = "fk_cobrador")]
        public long DebCollectorId { get; set; }

        [JsonProperty(PropertyName = "fk_cliente")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "fk_pedido")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "notas")]
        public String Notes { get; set; }

        [JsonProperty(PropertyName = "adeudo")]
        public double Outstanding { get; set; }
        #endregion

    }


}
