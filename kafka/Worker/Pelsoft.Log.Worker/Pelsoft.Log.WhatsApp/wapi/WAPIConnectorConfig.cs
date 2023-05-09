using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konectaAPI.wapi
{
    //Se crea esta nueva clase ya que la clase WAPIConnecto posee variables estáticas que en caso
    //de haber concuerrencia de hilos con diferentes configuraciones puede generar resultados
    //incorrectos, es decir, un hilo puede pisar la configuración de otro generando condiciones de carrera.
    //Se busca crear un clase que genere instacias únicas para cada hilo.
    public class WAPIConnectorConfig
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string CurrenToken { get; set; }
        public DateTime ExpiredDate { set; get; }
        public string Url { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyName { get; set; }
        public bool ProxyEnabled { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPassword { get; set; }
        public string ProxyDomain { get; set; }
    }
}
