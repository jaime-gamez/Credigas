using System;
namespace Credigas.Models
{
    public enum CitiesEnum{
        None,
        Mazatlan,
        Rosario,
        Escuinapa,
        Culiacan
    }

    public static class CityConverter{
        public static CitiesEnum ToCitiesEnum(string city){
            CitiesEnum result = CitiesEnum.None;

            switch (city.ToLowerInvariant())
            {
                case "mazatlán":
                    result = CitiesEnum.Mazatlan;
                    break;
                case "culiacán":
                    result = CitiesEnum.Culiacan;
                    break;
                case "rosario":
                    result = CitiesEnum.Rosario;
                    break;
                case "escuinapa":
                    result = CitiesEnum.Escuinapa;
                    break;
                default:
                    result = CitiesEnum.None;
                    break;
            }

            return result;
        }

        public static string ToString(CitiesEnum city)
        {
            string result = "";

            switch (city)
            {
                case CitiesEnum.Mazatlan:
                    result = "Mazatlán";
                    break;
                case CitiesEnum.Culiacan :
                    result = "Culiacán";
                    break;
                case CitiesEnum.Rosario:
                    result ="Rosario" ;
                    break;
                case CitiesEnum.Escuinapa:
                    result ="Escuinapa" ;
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        }
    }

    public class SigninModel
    {
        private string _user;
        public string User
        {
            get => _user;
            set => _user = value;
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        private CitiesEnum _city;
        public CitiesEnum City
        {
            get => _city;
            set => _city = value;
        }

        private bool _automaticSession;
        public bool AutomaticSession
        {
            get => _automaticSession;
            set => _automaticSession = value;
        }

    }
}
