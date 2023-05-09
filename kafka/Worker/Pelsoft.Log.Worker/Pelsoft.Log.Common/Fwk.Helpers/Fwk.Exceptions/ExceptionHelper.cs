using System.Text;


namespace Fwk.Exceptions
{
    /// <summary>
    /// Enumeracion que representa los tipos de excepciones de 
    /// </summary>
    public enum FwkExceptionTypes
    {
        /// <summary>
        /// TechnicalException
        /// </summary>
        TechnicalException = 0,
        /// <summary>
        /// FunctionalException
        /// </summary>
        FunctionalException = 1,
        /// <summary>
        /// DispatcherTecnicalExeption
        /// </summary>
        DispatcherTecnicalExeption = 2,
        /// <summary>
        /// WrapperConnectionsException
        /// </summary>
        WrapperConnectionsException = 3,
        /// <summary>
        /// BlockingFunctionalException
        /// </summary>
        BlockingFunctionalException = 4,
        /// <summary>
        /// OtherException
        /// </summary>
        OtherException = 5
    }
    /// <summary>
    /// Clase que procesa excepciones.
    /// </summary>
    /// <Author>moviedo</Author>
    /// <Date>28-12-2005</Date>
    public class ExceptionHelper
    {
        #region --[Public Static Methods]--



        ///// <summary>
        ///// Procesa la excepcion original y la retorna.
        ///// </summary>
        ///// <param name="pexception">Excepcion original.</param>
        ///// <returns>Excepcion procesada.</returns>
        //public static Exception ProcessException(Exception pexception)
        //{
        //    return ProcessException(pexception, null);
        //}
       

    

     
        /// <summary>
        /// Genera un string con el contenido del InnerException .-
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="includeStackTrace"></param>
        /// <returns></returns>
        public static String GetAllMessageException(Exception ex, bool includeStackTrace = true)
        {
            StringBuilder wMessage = new StringBuilder();
            wMessage.Append(ex.Message);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                wMessage.AppendLine(String.Concat("Source: ",ex.Source));
                wMessage.AppendLine();
                wMessage.AppendLine("Message: ");
                wMessage.AppendLine(ex.Message);
            }
            if (includeStackTrace && !String.IsNullOrEmpty(ex.StackTrace))
            {
                wMessage.AppendLine("\r\n-----------StackTrace------------------\r\n");
                wMessage.AppendLine(ex.StackTrace);
            }
            return wMessage.ToString();
        }

        /// <summary>
        /// Genera un string con el contenido del InnerException .-
        /// </summary>
        /// <param name="pExceptions">Array de excepciones</param>
        /// <returns></returns>
        public static String GetAllMessageException(Exception[] pExceptions)
        {
            StringBuilder wMessage = new StringBuilder();
            Exception inner;
            foreach (Exception ex in pExceptions)
            {
                wMessage.Append(ex.Message);
                while (ex.InnerException != null)
                {
                    inner = ex.InnerException;
                    wMessage.AppendLine(String.Concat("Source: ", inner.Source));

                    wMessage.AppendLine();
                    wMessage.AppendLine("Message: ");
                    wMessage.AppendLine(inner.Message);
                }
            }
            return wMessage.ToString();
        }

        /// <summary>
        /// Retorna el tipo <see cref="FwkExceptionTypes"/> de acuerdo al ex.GetType() de la excepción poasada por parametro
        /// </summary>
        /// <param name="ex">excepción</param>
        /// <returns><see cref="FwkExceptionTypes"/></returns>
        public static FwkExceptionTypes GetFwkExceptionTypes(Exception ex)
        {
            //if (ex.GetType() == typeof(FunctionalException))
            //{

            //    return FwkExceptionTypes.FunctionalException;
            //}
            if (ex.GetType() == typeof(TechnicalException))
            {
                return FwkExceptionTypes.TechnicalException;
            }

            return FwkExceptionTypes.OtherException;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetFwkExceptionTypesName(Exception ex)
        {
            //if (ex.GetType() == typeof(FunctionalException))
            //{
            //    return  FwkExceptionTypes.FunctionalException.ToString();
            //}
            if (ex.GetType() == typeof(TechnicalException))
            {
                return  FwkExceptionTypes.TechnicalException.ToString();
            }

            return FwkExceptionTypes.OtherException.ToString();

        }
        /// <summary>
        /// Retorna el error id de la excepción
        /// </summary>
        /// <param name="ex">excepción</param>
        /// <returns>ErrorId de la excepción </returns>
        public static string GetFwkErrorId(Exception ex)
        {
           
            //if (GetFwkExceptionTypes(ex) == FwkExceptionTypes.FunctionalException || ex.GetType().BaseType == typeof(FunctionalException)) 
            //{
            //    return ((FunctionalException)ex).ErrorId;
            //}
            if (GetFwkExceptionTypes(ex) == FwkExceptionTypes.TechnicalException || ex.GetType().BaseType == typeof(TechnicalException)) 
            {
                return ((TechnicalException)ex).ErrorId;
            }
            return string.Empty;

        }
       

        #endregion

        #region --[Private Static Methods]--

        /// <summary>
        /// Procesa los de SqlServer con el formato "Identificador_Mensaje;Param1;Param2...".
        /// </summary>
        /// <param name="pSqlEx">Mensaje de error con formato "Identificador_Mensaje;Param1;Param2...".</param>
        /// <param name="pParams">Parametros a reemplazar en el mensaje.</param>
        /// <returns>Mensaje de error.</returns>
        private static string ProcessRaiseErrorMsg(string pSqlEx, out string[] pParams)
        {
            try
            {
                /* Formato RAISEERROR: "Identificador_Mensaje;Param1;Param2 */
                string[] wRaiseErrorMsg = pSqlEx.Split(';');
                int wParamsLenght = wRaiseErrorMsg.Length - 1;

                // Obtiene los parametros;
                pParams = new string[wParamsLenght];
                for (int i = 0; i < wParamsLenght; i++)
                {
                    pParams[i] = wRaiseErrorMsg[i + 1];
                }

                // Retorna el MsgId.
                return wRaiseErrorMsg[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       


        #endregion


        /// <summary>
        /// Establece los valores basicos de error producido en el componente ADHelper
        /// </summary>
        /// <param name="te"></param>
        public static void SetTechnicalException<T>(Fwk.Exceptions.TechnicalException te)
        {
            //te.Namespace = typeof(T).Namespace;
            //te.Assembly = typeof(T).Assembly.FullName;
            te.Class = typeof(T).GetType().Name;
            te.UserName = Environment.UserName;
            te.Machine = Environment.MachineName;
            //te.Source =  ConfigurationsHelper.HostApplicationName;
        }
         /// <summary>
        /// Establece los valores basicos de error producido en el componente ADHelper
        /// </summary>
        /// <param name="te"></param>
        public static void SetTechnicalException(Fwk.Exceptions.TechnicalException te,Type T)
        {
            //te.Namespace = T.Namespace;
            //te.Assembly = T.Assembly.FullName;
            te.Class = T.GetType().Name;
            te.UserName = Environment.UserName;
            te.Machine = Environment.MachineName;
            //te.Source = ConfigurationsHelper.HostApplicationName;
        }
    
    }
}
