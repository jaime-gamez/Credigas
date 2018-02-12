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

        [ForeignKey(typeof(Order))] // Specify the foreign key
        [JsonProperty(PropertyName = "fk_pedido")]
        public long OrderId { get; set; }

        [ManyToOne]  // Many to one relationship with Order
        public Order Order { get; set; }

        [JsonProperty(PropertyName = "monto")]
        public double Total { get; set; }

        [JsonProperty(PropertyName = "sincronizado")]
        public bool IsSync { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }
        #endregion

        #region Ignore
        [Ignore]
        public String CardId
        {
            get
            {
                return Order?.CardId;
            }

        }

        [Ignore]
        public String FullName
        {
            get
            {
                return Order?.Customer?.FullName;
            }

        }

        [Ignore]
        public String Icon
        {
            get
            {
                return Order?.Customer?.Icon;
            }

        }
        #endregion
    }
}
