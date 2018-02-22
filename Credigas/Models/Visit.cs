using System;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Xamarin.Forms;

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

        [ForeignKey(typeof(Customer))]  // Specify the foreign key
        [JsonProperty(PropertyName = "fk_cliente")]
        public long CustomerId { get; set; }

        [JsonIgnore]
        [ManyToOne]      // Many to one relationship with Customer
        public Customer Customer { get; set; }

        [JsonProperty(PropertyName = "fk_pedido")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "notas")]
        public String Notes { get; set; }

        [JsonProperty(PropertyName = "adeudo")]
        public double Outstanding { get; set; }

        [JsonProperty(PropertyName = "sincronizado")]
        public int IsSync { get; set; }

        [Ignore]
        public Color TextColor
        {
            get
            {
                if (IsSync == 0)
                {
                    return Color.FromHex("#FF5521");
                }
                return Color.FromHex("#012C40");
            }
        }

        [JsonIgnore]
        [Ignore]
        public String FullName
        {
            get
            {
                return Customer?.FullName;
            }

        }

        [JsonIgnore]
        [Ignore]
        public String Icon
        {
            get
            {
                return Customer?.Icon;
            }

        }
        #endregion

    }


}
