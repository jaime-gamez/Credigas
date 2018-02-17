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

        [JsonIgnore]
        [ManyToOne]  // Many to one relationship with Order
        public Order Order { get; set; }

        [JsonProperty(PropertyName = "monto")]
        public double Total { get; set; }

        [JsonProperty(PropertyName = "sincronizado")]
        public int IsSync { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }
        #endregion

        #region Ignore
        [JsonIgnore]
        [Ignore]
        public String CardId
        {
            get
            {
                return Order?.CardId;
            }

        }

        [JsonIgnore]
        [Ignore]
        public String FullName
        {
            get
            {
                return Order?.Customer?.FullName;
            }

        }

        [JsonIgnore]
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
