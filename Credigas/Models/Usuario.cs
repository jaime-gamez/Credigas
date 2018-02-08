namespace Credigas.Models
{
    using System;
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;
    public class Usuario
    {
        #region Properties
        [PrimaryKey]
        [JsonProperty(PropertyName = "id_usuario")]
        public int UsuarioId { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "ape_paterno")]
        public int ApellidoPaterno { get; set; }

        [JsonProperty(PropertyName = "ape_materno")]
        public string ApellidoMaterno { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
