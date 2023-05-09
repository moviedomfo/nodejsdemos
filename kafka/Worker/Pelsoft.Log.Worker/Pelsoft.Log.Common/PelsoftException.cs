using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Pelsoft.Log.Common
{
    
    /// <summary>
    /// MAnejo de Excepciones propias de epiron
    /// </summary>
    public class PelsoftException : ApplicationException
    {
        /// <summary>
        /// Lista de Ids de errores que provocan un Stop del proceso
        ///
        /// </summary>
        public static int[] Process_details_settings_FatalError = new int[] { 1310, 1311, 1400 };

        /// <summary>
        /// No se concideran Errores
        /// 
        /// 1303: No se puede cerrar ProcessDetails
        /// </summary>
        public static int[] Process_details_settings_warnings = new int[] { 1300, 1302, 1301,1303 };

        // Summary:
        //     Assembly.
        public string AssemblyLocation { get; set; }

        //
        // Summary:
        //     Class.
        public string Class { get; set; }

        //public String 
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the database associated with the connection.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name or network address of the instance of SQL Server to
        ///     connect to.
        /// </summary>
        public string DatabaseServerName { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        //protected string mMsg;
        /// <summary>
        /// Excepcion tecnica.
        /// </summary>
        /// <param name="pmsg">Mensaje del error.</param>
        /// <param name="pinner">Excepcion original.</param>
        public PelsoftException(string pmsg, Exception pinner)
            : base(pmsg, pinner)
        {
            //mMsg = pmsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static PelsoftException ProcessException(Exception ex, Type sourceObjectType, String message, String connectionString)
        {
            PelsoftException exception = null;

            if (ex != null && typeof(PelsoftException) == ex.GetType())
                return (PelsoftException)ex;

            if (ex == null)
            {
                exception = new PelsoftException(message, null);
            }
            else
            {
                if (ex is System.Data.SqlClient.SqlException && exception == null)
                {
                    exception = new PelsoftException(String.Concat("SqlException : ", message, "\r\n", ex.Message), ex);

                    exception.ErrorCode = ((SqlException)ex).ErrorCode;
                }

                if (ex.GetType().Name.Contains("Pop3Exception") && exception == null)
                {

                    exception = new PelsoftException(String.Concat("Pop3Exception : ", message, "\r\n", ex.Message), ex);
                }
            }

            if (exception == null && ex!=null)
            {
                exception = new PelsoftException(String.Concat(message, "\r\n", ex.Message), ex);
            }

            if (!String.IsNullOrEmpty(connectionString))
            {
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(connectionString);
                exception.DatabaseServerName = sqlBuilder.DataSource;
                exception.DatabaseName = sqlBuilder.InitialCatalog;
            }

            if (sourceObjectType != null)
            {
                exception.AssemblyLocation = sourceObjectType.Assembly.Location;
                exception.Class = sourceObjectType.Name;
            }

            return exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="sourceObject"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static PelsoftException ProcessException(Exception ex, Type sourceObjectType, string connectionString)
        {
            PelsoftException exception = PelsoftException.ProcessException(ex, sourceObjectType, String.Empty, connectionString);
            
            return exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static PelsoftException ProcessException(Exception ex, object sourceObject)
        {
            return PelsoftException.ProcessException(ex, sourceObject.GetType(), String.Empty, String.Empty);
        }
    }
}
