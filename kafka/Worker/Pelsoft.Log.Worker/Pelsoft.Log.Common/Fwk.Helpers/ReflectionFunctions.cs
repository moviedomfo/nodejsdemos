using Fwk.Exceptions;
using System.Reflection;

namespace Fwk.HelperFunctions
{
    public class ReflectionFunctions
    {

      

        /// <summary>
        /// Crea instancia de un objetos a partir de de su nombre largo
        /// 
        /// </summary>
        /// <param name="pAssemblyString">String separado por comas "Type.FullName,Assembly.Name"</param>
        /// <returns>Instancia del objeto</returns>
        static public object CreateInstance(string pAssemblyString)
        {
            return CreateInstance(pAssemblyString, null);
        }

        /// <summary>
        /// El método CreateInstance crea una instancia de un tipo definido en un ensamblado llamando al constructor que mejor coincida con los argumentos especificados. Si no se ha especificado 
        /// ningún argumento, se llama al constructor que no toma parámetros, es decir, el constructor predeterminado.
        /// </summary>
        /// <param name="pAssemblyString">String separado por comas "Type.FullName,Assembly.Name"</param>
        /// <param name="constructorParams">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. 
        /// If args is an empty array or nullNothingnullptra null reference (Nothing in Visual Basic), 
        /// the constructor that takes no parameters (the default constructor) is invoked. </param>
        /// <returns>Instancia del objeto</returns>
        public static object CreateInstance(string pAssemblyString, object[] constructorParams)
        {
            string wClassName;
            string wAssembly;

            ExtractTypeInformation(pAssemblyString, out wClassName, out wAssembly);

            // crea el objeto dinámicamente
            object wResult;
            if (constructorParams == null)
                wResult = CreateInstanceLoad(wClassName, wAssembly);
            else
                wResult = CreateInstanceLoad(wClassName, wAssembly, constructorParams);

            return wResult;

        }

        /// <summary>
        /// Crea instancia de un objetos a partir de de su nombre largo y sus parametros.
        /// Efectua load assembly
        /// </summary>
        /// <param name="pClassName">Nombre de la clase (Type.FullName)</param>
        /// <param name="pAssemblyName">Nombre del ensamblado (Assembly.Name)</param>
        /// <returns>Instancia del objeto</returns>
        public static object CreateInstanceLoad(string pClassName, string pAssemblyName)
        {
            Assembly wAssembly = Assembly.Load(pAssemblyName);
            object wResult = wAssembly.CreateInstance(pClassName, true);

            return wResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pAssemblyString">El parametro pAssemblyString espera el siguiente formato : [Namespase.ClassName ,Assembly]</param>
        /// <param name="pClassName"></param>
        /// <param name="pAssembly"></param>
        private static void ExtractTypeInformation(string pAssemblyString, out string pClassName, out string pAssembly)
        {
            pClassName = String.Empty;
            pAssembly = String.Empty;
            TechnicalException te = null;
            try
            {
                // Divide el assemblyString según la coma y guarda el resultado
                // en un array
                string[] wParts = GetStringsFromAssemblyString(pAssemblyString);

                // Verifica que el array tenga 2 partes
                if (wParts.Length < 2)
                {
                    te = new TechnicalException("Assembly mal configurado. El parametro pAssemblyString espera el siguiente formato : [Namespase.ClassName ,Assembly]");
                    ExceptionHelper.SetTechnicalException<ReflectionFunctions>(te);
                    te.ErrorId = "3000";
                }

                // Toma las partes del assemblyArray en dos strings separados
                pClassName = wParts[0].Trim();
                pAssembly = wParts[1].Trim();

                // Verifica que el string strNamespaceClass tenga al menos un punto
                if (pClassName.IndexOf(".") < 0)
                {
                    te = new TechnicalException("Assembly mal configurado. El parametro pAssemblyString espera el siguiente formato : [Namespase.ClassName ,Assembly]");
                    ExceptionHelper.SetTechnicalException<ReflectionFunctions>(te);
                    te.ErrorId = "3000";
                }
            }
            catch (Exception ex)
            {

                te = new TechnicalException("Assembly mal configurado. El parametro pAssemblyString espera el siguiente formato : [Namespase.ClassName ,Assembly]", ex);
                ExceptionHelper.SetTechnicalException<ReflectionFunctions>(te);
                te.ErrorId = "3000";
            }
        }
        /// <summary>
        /// Crea instancia de un objetos a partir de de su nombre largo y sus parametros.
        /// Efectua load assembly
        /// </summary>
        /// <param name="pClassName">Nombre de la clase (Type.FullName)</param>
        /// <param name="pAssemblyName">Nombre del ensamblado (Assembly.Name)</param>
        /// <param name="constructorParams">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. 
        /// If args is an empty array or nullNothingnullptra null reference (Nothing in Visual Basic), 
        /// the constructor that takes no parameters (the default constructor) is invoked. </param>
        /// <returns></returns>
        public static object CreateInstanceLoad(string pClassName, string pAssemblyName, object[] constructorParams)
        {

            Type wType = CreateType(pClassName, pAssemblyName);

            return Activator.CreateInstance(wType, constructorParams);

        }


        /// <summary>
        /// Crea un typo cualquiera 
        /// </summary>
        /// <param name="pClassName">Type.FullName</param>
        /// <param name="pAssemblyName">Assembly.Name</param>
        /// <returns>Type</returns>
        static public Type CreateType(string pClassName, string pAssemblyName)
        {
            Assembly wAssembly = Assembly.Load(pAssemblyName);
            Type wResult = wAssembly.GetType(pClassName, true);

            return wResult;
        }
        static private string[] GetStringsFromAssemblyString(string pAssemblyString)
        {
            return pAssemblyString.Split(',');

        }
    }
}
