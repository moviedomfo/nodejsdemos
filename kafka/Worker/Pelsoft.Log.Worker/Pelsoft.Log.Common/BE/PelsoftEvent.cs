using Fwk.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common
{
    public class Event
    {
        #region <private members>
        private String _AppId = string.Empty;
        private String _Source = string.Empty;
        private EventType _LogType;
        private Guid _Id;
        private string _UserLoginName = string.Empty;
        private string _Machine = string.Empty;
        private string _Message = string.Empty;
        private DateTime _LogDate;
        #endregion

        #region <constructor>
        /// <summary>
        /// Constructor de Event que inicializa todos los atributos como nulos o empty 
        /// Valor por defecto:  
        ///  Id es autogenerado
        /// </summary>
        public Event()
        {
            _Id = Guid.NewGuid();
            _LogDate = Fwk.HelperFunctions.DateFunctions.NullDateTime;
            _LogType = EventType.None;
        }
        /// <summary>
        /// Constructor de Event.
        /// Valor por defecto:     
        ///   Id es autogenerado 
        ///   Machine = Environment.MachineName;
        ///   UserLoginName = Environment.UserName;
        ///   LogDate = DateTime.Now;
        /// </summary>
        /// <param name="pType">Tipo de evento.</param>
        /// <param name="pSource">Origen del evento.</param>
        /// <param name="pMessage">Mensaje descriptivo del evento.</param>
        public Event(EventType pType, string pSource, string pMessage)
        {
            _Id = Guid.NewGuid();
            _LogType = pType;
            _Source = pSource;
            _Message = pMessage;
            _Machine = Environment.MachineName;
            _UserLoginName = Environment.UserName;
            _LogDate = DateTime.Now;
        }

        /// <summary>
        /// Constructor de Event.
        /// Valor por defecto:     
        ///   Id es autogenerado
        ///   LogDate = DateTime.Now;
        /// </summary>
        /// <param name="pType">Tipo de evento.</param>
        /// <param name="pSource">Origen del evento.</param>
        /// <param name="pMessage">Mensaje descriptivo del evento.</param>
        /// <param name="pMachine">Equipo donde se origina el evento.</param>
        /// <param name="pUserName">Usuario que origina el evento.</param>
        public Event(EventType pType, string pSource, string pMessage, string pMachine, string pUserName)
        {
            _Id = Guid.NewGuid();
            _LogType = pType;
            _Source = pSource;
            _Message = pMessage;
            _Machine = pMachine;
            _UserLoginName = pUserName;
            _LogDate = DateTime.Now;
        }

        /// <summary>
        ///  Constructor de Event. 
        /// Valor por defecto:     
        ///   Id es autogenerado
        ///   LogDate = DateTime.Now;
        /// </summary>
        /// <param name="pType">Tipo de evento.</param>
        /// <param name="pSource">Origen del evento.</param>
        /// <param name="pMessage">Mensaje descriptivo del evento.</param>
        /// <param name="pMachine">Equipo donde se origina el evento.</param>
        /// <param name="pUserName">Usuario que origina el evento.</param>
        /// <param name="appId">Identificador de la aplicación que genera el log</param>
        public Event(EventType pType, string pSource, string pMessage, string pMachine, string pUserName, string appId)
        {
            _Id = Guid.NewGuid();
            _AppId = appId;
            _LogType = pType;

            _Source = pSource;
            _Message = pMessage;
            _Machine = pMachine;
            _UserLoginName = pUserName;
            _LogDate = DateTime.Now;
        }

        #endregion

        #region <public properties>
        /// <summary>
        /// Mensaje descriptivo del evento.
        /// </summary>
     
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary>
        /// Fecha y hora en la que se produce el evento.
        /// </summary>
        
        public DateTime LogDate
        {
            get { return _LogDate; }
            set { _LogDate = value; }
        }
    
        public string Machine
        {
            get { return _Machine; }
            set { _Machine = value; }
        }

    
        public string User
        {
            get { return _UserLoginName; }
            set { _UserLoginName = value; }
        }

   
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

      
        public EventType LogType
        {
            get { return _LogType; }
            set { _LogType = value; }
        }

        /// <summary>
        /// Origen del evento.
        /// </summary>
        public String Source
        {
            get
            {
                //if (string.IsNullOrEmpty(_Source))
                //    _Source = ConfigurationsHelper.HostApplicationName;

                return _Source;
            }
            set { _Source = value; }
        }
        /// <summary>
        /// Identificador de la aplicacion o sistema qhe genera el log.-
        /// </summary>
        public String AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }
        #endregion

        #region <public methods>
        /// <summary>
        /// Recopila la información de todos los atributos y 
        /// genera una cadena de texto con ella.
        /// </summary>
        /// <returns>Cadena de texto.</returns>
        public override string ToString()
        {
            StringBuilder wStringBuilder = new StringBuilder();
            wStringBuilder.Append(string.Concat("Log Id: ", this._Id));
            wStringBuilder.AppendLine();
            wStringBuilder.Append(" | Date: ");
            wStringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture));
            wStringBuilder.AppendLine(string.Concat(" Type: ", _LogType.ToString().ToUpper()));

            wStringBuilder.AppendLine("Message: ");
            wStringBuilder.AppendLine(this._Message);
            wStringBuilder.AppendLine(" Source: ");
            wStringBuilder.Append(this._Source);
            wStringBuilder.AppendLine(" User: ");
            wStringBuilder.Append(this._UserLoginName);
            wStringBuilder.AppendLine(" Machine: ");
            wStringBuilder.Append(this._Machine);
            wStringBuilder.AppendLine(" Applcation Id: ");
            wStringBuilder.Append(_AppId);
            return wStringBuilder.ToString();
        }


        #endregion
    }

    public class PelsoftEvent : Event
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ServiceInstanceUnique { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? AsiDetailUnique { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? OriginalGUID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ConnectionString { get; set; }

        /// <summary>
        /// Identificador del error respecto al sistema. No representa el id de base de datos
        /// </summary>
        public String PelsoftLogId { get; set; }

        public Int32? POriginalTypeId { get; set; }
    }

 
}
