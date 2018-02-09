namespace Credigas.Models
{
    using System;
    using Newtonsoft.Json;
    using SQLite;
    using SQLiteNetExtensions.Attributes;

    public class Payment
    {
        #region Properties
        [PrimaryKey]
        [JsonProperty(PropertyName = "id_abono")]
        public long PaymentId { get; set; }

        [ForeignKey(typeof(Order))]
        [JsonProperty(PropertyName = "fk_pedido")]
        public long OrderId { get; set; }

        [ManyToOne]
        public Order Order { get; set; }

        [JsonProperty(PropertyName = "monto")]
        public double Total { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }
        #endregion
        }
}
