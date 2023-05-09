using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;



namespace Fwk.Exceptions
{
    /// <summary>
    /// Excepcion Tecnica.
    /// </summary>
    /// <Author>moviedo</Author>
    /// <Date>28-12-2005</Date>
    [ComVisible(false)]
    [Serializable()]
    public class TechnicalException : ApplicationException
    {
        #region --[Protected Constants]--
        /// <summary>
        /// EXCEPTIONSMESSAGES_GROUPNAME.
        /// </summary>
        protected const string EXCEPTIONSMESSAGES_GROUPNAME = "ExceptionMessages";
        
        /// <summary>
        /// TECHNICALMESSAGE_ATTRIBUTENAME
        /// </summary>
        protected const string TECHNICALMESSAGE_ATTRIBUTENAME = "TechExceptionMsg";
        #endregion

        #region --[Protected Vars]--
        private String _ErrorId = String.Empty;

        /// <summary>
        /// Assembly.
        /// </summary>
        protected string mAssembly;

        /// <summary>
        /// Namespace.
        /// </summary>
        protected string mNamespace;

        /// <summary>
        /// Class.
        /// </summary>
        protected string mClass;

        /// <summary>
        /// Machine.
        /// </summary>
        protected string mMachine;

        /// <summary>
        /// UserName.
        /// </summary>
        protected string mUserName;

        /// <summary>
        /// Message.
        /// </summary>
        protected string mMsg;
        #endregion

        #region --[Public Properties]--

        /// <summary>
        /// 
        /// </summary>
        public String ServiceName { get; set; }
        /// <summary>
        /// Identificador del error
        /// </summary>
        public String ErrorId
        {
            get { return _ErrorId; }
            set { _ErrorId = value; }
        }

        /// <summary>
        /// Assembly donde se produce la excepcion.
        /// </summary>
        public string Assembly
        {
            get { return mAssembly; }
            set { mAssembly = value; }
        }
        /// <summary>
        /// Namespace de la clase donde se produce la excepcion.
        /// </summary>
        public string Namespace
        {
            get { return mNamespace; }
            set { mNamespace = value; }
        }

        /// <summary>
        /// Clase donde se produce la excepcion.
        /// </summary>
        public string Class
        {
            get { return mClass; }
            set { mClass = value; }
        }

        /// <summary>
        /// Nombre del Equipo del cliente donde se produce la excepcion.
        /// </summary>
        public string Machine
        {
            get { return mMachine; }
            set { mMachine = value; }
        }

        /// <summary>
        /// Nombre del usuario cliente.
        /// </summary>
        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }

        /// <summary>
        /// Message.
        /// </summary>
        public override string Message
        {
            get { return mMsg; }
        }

        #endregion

        #region --[Constructors]--
        /// <summary>
        /// Excepcion tecnica.
        /// </summary>
        public TechnicalException()
        {
        }

        /// <summary>
        /// Excepcion tecnica.
        /// </summary>
        /// <param name="pmsg">Mensaje del error.</param>
        public TechnicalException(string pmsg) : base(pmsg)
        {
            mMsg = pmsg;
            //this.Source = ConfigurationsHelper.HostApplicationName;
        }

        /// <summary>
        /// Excepcion tecnica.
        /// </summary>
        /// <param name="pmsg">Mensaje del error.</param>
        /// <param name="pinner">Excepcion original.</param>
        public TechnicalException(string pmsg, Exception pinner) : base(pmsg, pinner)
        {
            mMsg = pmsg;
            //this.Source = ConfigurationsHelper.HostApplicationName;
        }


        #endregion

        #region --[Protected Methods]--
        /// <summary>
        /// Agrega texto al comienzo del mensaje
        /// </summary>
        /// <param name="pmsg"></param>
        public void AddMessage_Top(string pmsg)
        {
            mMsg = string.Concat(pmsg, Environment.NewLine, mMsg);
        }
        /// <summary>
        /// Agrega texto al final del mensae
        /// </summary>
        /// <param name="pmsg"></param>
        public void AddMessage_Down(string pmsg)
        {
            mMsg = string.Concat(mMsg, Environment.NewLine, pmsg);
        }
     
        #endregion




    }
}