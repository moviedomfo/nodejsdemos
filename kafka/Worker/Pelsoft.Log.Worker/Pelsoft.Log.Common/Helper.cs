using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using Fwk.HelperFunctions;
using Fwk.Exceptions;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Pelsoft.Log.Common.Services;
using Fwk.Logging;
using Pelsoft.Log.Common.Workers;

namespace Pelsoft.Log.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {
        private static ICnnStringService _CnnStringService;

        
        
        //public static ServiceInstance_Struct Instance { get; set; }
        //static bool performLog = true;
        public static String email_log_body = "Email_Log.htm";

        //static Helper()
        //{
            //Fwk.HelperFunctions.DateFunctions.BeginningOfTimes = new DateTime(1753, 1, 1);
            ///TODO: migracion Ver como obtener email_log_body Email_Log.htm
            //string fullFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Email_Log.htm");
            //email_log_body = Fwk.HelperFunctions.FileFunctions.OpenTextFile(fullFileName);
        //}
        //public static void InitHelpers(ICnnStringService cnnStringService)
        //{
        //    _CnnStringService = cnnStringService;
        //}

     

        /// <summary>
        /// Extrae las urls que esten contenidas en una cadena de texto
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>
        public static List<string> ExtractUrls(string pString)
        {
            string wPattern = @"(http|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            return Regex.Matches(pString, wPattern).Cast<Match>().ToList().Select(m => m.Value).ToList<string>();
        }

        /// <summary>
        /// Minimifica las urls en un texto
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>
        public static async Task<string> MinifyUrls(string pString)
        {
            string wTinyUrl = string.Empty, wResultString = pString;
            List<string> wUrls = ExtractUrls(pString);
            if (wUrls == null || wUrls.Count == 0)
                return pString;

            foreach (string url in wUrls)
            {
                wTinyUrl = await TinyUrl.ShrinkURL(url);
                wResultString = wResultString.Replace(url, wTinyUrl);
            }

            return wResultString;
        }

        /// <summary>
        /// Minimifica las urls en un texto. NO devuelve un Task pero sigue siendo async
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>
        public static string MinifyTextUrls(string pString)
        {
            string wTinyUrl = string.Empty, wResultString = pString;
            List<string> wUrls = ExtractUrls(pString);
            if (wUrls == null || wUrls.Count == 0)
                return pString;

            foreach (string url in wUrls)
            {
                wTinyUrl = TinyUrl.ShrinkURL(url).Result;
                wResultString = wResultString.Replace(url, wTinyUrl);
            }

            return wResultString;
        }

        /// <summary>
        /// Metodo para convertir los caracteres unicode a ascii.
        /// Tabla de ASCII estándar, nombres de entidades HTML, ISO 10646, ISO 8879, ISO 8859-1 romano 1. Soporte para browsers: todos los browsers
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        public static string convertTextUnicode(string pText)
        {
            //% --> Codigo: 0x0025
            if (pText.Contains("0x0025"))
                pText = pText.Replace("0x0025", "%");
            //< --> Codigo: 0x003C
            if (pText.Contains("0x003c"))
                pText = pText.Replace("0x003c", "<");
            //@ --> Codigo: 0x0040
            if (pText.Contains("0x0040"))
                pText = pText.Replace("0x0040", "@");
            #region 00
            //! --> NO hace falta
            //" --> NO hace falta
            //# --> NO hace falta
            //$ --> NO hace falta
            //& --> NO hace falta
            //' --> NO hace falta
            //( --> NO hace falta
            //) --> NO hace falta
            //* --> NO hace falta
            //+ --> NO hace falta
            //, --> NO hace falta
            //- --> NO hace falta
            //. --> NO hace falta
            /// --> NO hace falta
            //0 --> NO hace falta
            //1 --> NO hace falta
            //2 --> NO hace falta
            //3 --> NO hace falta
            //4 --> NO hace falta
            //5 --> NO hace falta
            //6 --> NO hace falta
            //7 --> NO hace falta
            //8 --> NO hace falta
            //9 --> NO hace falta
            //: --> NO hace falta
            //; --> NO hace falta
            //= --> NO hace falta
            //> --> NO hace falta
            //? --> NO hace falta
            //A --> NO hace falta
            //B --> NO hace falta
            //C --> NO hace falta
            //D --> NO hace falta
            //E --> NO hace falta
            //F --> NO hace falta
            //G --> NO hace falta
            //H --> NO hace falta
            //I --> NO hace falta
            //J --> NO hace falta
            //K --> NO hace falta
            //L --> NO hace falta
            //M --> NO hace falta
            //N --> NO hace falta
            //O --> NO hace falta
            //P --> NO hace falta
            //Q --> NO hace falta
            //R --> NO hace falta
            //S --> NO hace falta
            //T --> NO hace falta
            //U --> NO hace falta
            //V --> NO hace falta
            //W --> NO hace falta
            //X --> NO hace falta
            //Y --> NO hace falta
            //Z --> NO hace falta
            //[ --> NO hace falta
            //\ --> NO hace falta
            //] --> NO hace falta
            //^ --> NO hace falta
            //_ --> NO hace falta
            //` --> NO hace falta
            //a --> NO hace falta
            //b --> NO hace falta
            //c --> NO hace falta
            //d --> NO hace falta
            //e --> NO hace falta
            //f --> NO hace falta
            //g --> NO hace falta
            //h --> NO hace falta
            //i --> NO hace falta
            //j --> NO hace falta
            //k --> NO hace falta
            //l --> NO hace falta
            //m --> NO hace falta
            //n --> NO hace falta
            //o --> NO hace falta
            //p --> NO hace falta
            //q --> NO hace falta
            //r --> NO hace falta
            //s --> NO hace falta
            //t --> NO hace falta
            //u --> NO hace falta
            //v --> NO hace falta
            //w --> NO hace falta
            //x --> NO hace falta
            //y --> NO hace falta
            //z --> NO hace falta
            //{ --> NO hace falta
            //| --> NO hace falta
            //} --> NO hace falta
            //~ --> NO hace falta
            #endregion
            #region a
            //--------------------------------------------------------
            //¡ --> Codigo: 0x00a1
            if (pText.Contains("0x00a1"))
                pText = pText.Replace("0x00a1", "¡");
            //¢ --> Codigo: 0x00a2
            if (pText.Contains("0x00a2"))
                pText = pText.Replace("0x00a2", "¢");
            //£ --> Codigo: 0x00a3
            if (pText.Contains("0x00a3"))
                pText = pText.Replace("0x00a3", "£");
            //¤ --> Codigo: 0x00a4
            if (pText.Contains("0x00a4"))
                pText = pText.Replace("0x00a4", "¤");
            //¥ --> Codigo: 0x00a5
            if (pText.Contains("0x00a5"))
                pText = pText.Replace("0x00a5", "¥");
            //¦ --> Codigo: 0x00a6
            if (pText.Contains("0x00a6"))
                pText = pText.Replace("0x00a6", "¦");
            //§ --> Codigo: 0x00a7
            if (pText.Contains("0x00a7"))
                pText = pText.Replace("0x00a7", "§");
            //¨ --> Codigo: 0x00a8
            if (pText.Contains("0x00a8"))
                pText = pText.Replace("0x00a8", "¨");
            //© --> Codigo: 0x00a9
            if (pText.Contains("0x00a9"))
                pText = pText.Replace("0x00a9", "©");
            //ª --> Codigo: 0x00aa
            if (pText.Contains("0x00aa"))
                pText = pText.Replace("0x00aa", "ª");
            //« --> Codigo: 0x00ab
            if (pText.Contains("0x00ab"))
                pText = pText.Replace("0x00ab", "«");
            //¬ --> Codigo: 0x00ac
            if (pText.Contains("0x00ac"))
                pText = pText.Replace("0x00ac", "¬");
            //® --> Codigo: 0x00ae
            if (pText.Contains("0x00ae"))
                pText = pText.Replace("0x00ae", "®");
            //¯ --> Codigo: 0x00af
            if (pText.Contains("0x00af"))
                pText = pText.Replace("0x00af", "¯");
            //--------------------------------------------------------
            #endregion
            #region b
            //° --> Codigo: 0x00b0
            if (pText.Contains("0x00b0"))
                pText = pText.Replace("0x00b0", "°");
            //± --> Codigo: 0x00b1
            if (pText.Contains("0x00b1"))
                pText = pText.Replace("0x00b1", "±");
            //² --> Codigo: 0x00b2
            if (pText.Contains("0x00b2"))
                pText = pText.Replace("0x00b2", "²");
            //³ --> Codigo: 0x00b3
            if (pText.Contains("0x00b3"))
                pText = pText.Replace("0x00b3", "³");
            //´ --> Codigo: 0x00b4
            if (pText.Contains("0x00b4"))
                pText = pText.Replace("0x00b4", "´");
            //µ --> Codigo: 0x00b5
            if (pText.Contains("0x00b5"))
                pText = pText.Replace("0x00b5", "µ");
            //¶ --> Codigo: 0x00b6
            if (pText.Contains("0x00b6"))
                pText = pText.Replace("0x00b6", "¶");
            //· --> Codigo: 0x00b7
            if (pText.Contains("0x00b7"))
                pText = pText.Replace("0x00b7", "·");
            //¸ --> Codigo: 0x00b8
            if (pText.Contains("0x00b8"))
                pText = pText.Replace("0x00b8", "¸");
            //¹ --> Codigo: 0x00b9
            if (pText.Contains("0x00b9"))
                pText = pText.Replace("0x00b9", "¹");
            //º --> Codigo: 0x00ba
            if (pText.Contains("0x00ba"))
                pText = pText.Replace("0x00ba", "º");
            //» --> Codigo: 0x00bb
            if (pText.Contains("0x00bb"))
                pText = pText.Replace("0x00bb", "»");
            //¼ --> Codigo: 0x00bc
            if (pText.Contains("0x00bc"))
                pText = pText.Replace("0x00bc", "¼");
            //½ --> Codigo: 0x00bd
            if (pText.Contains("0x00bd"))
                pText = pText.Replace("0x00bd", "½");
            //¾ --> Codigo: 0x00be
            if (pText.Contains("0x00be"))
                pText = pText.Replace("0x00be", "¾");
            //¿ --> Codigo: 0x00bf
            if (pText.Contains("0x00bf"))
                pText = pText.Replace("0x00bf", "¿");
            //--------------------------------------------------------
            #endregion
            #region c
            //À --> Codigo: 0x00c0
            if (pText.Contains("0x00c0"))
                pText = pText.Replace("0x00c0", "À");
            //Á --> Codigo: 0x00c1
            if (pText.Contains("0x00c1"))
                pText = pText.Replace("0x00c1", "Á");
            //Â --> Codigo: 0x00c2
            if (pText.Contains("0x00c2"))
                pText = pText.Replace("0x00c2", "Â");
            //Ã --> Codigo: 0x00c3
            if (pText.Contains("0x00c3"))
                pText = pText.Replace("0x00c3", "Ã");
            //Ä --> Codigo: 0x00c4
            if (pText.Contains("0x00c4"))
                pText = pText.Replace("0x00c4", "À");
            //Å --> Codigo: 0x00c5
            if (pText.Contains("0x00c5"))
                pText = pText.Replace("0x00c5", "Å");
            //Æ --> Codigo: 0x00c6
            if (pText.Contains("0x00c6"))
                pText = pText.Replace("0x00c6", "Æ");
            //Ç --> Codigo: 0x00c7
            if (pText.Contains("0x00c7"))
                pText = pText.Replace("0x00c7", "Ç");
            //È --> Codigo: 0x00c8
            if (pText.Contains("0x00c8"))
                pText = pText.Replace("0x00c8", "È");
            //É --> Codigo: 0x00c9
            if (pText.Contains("0x00c9"))
                pText = pText.Replace("0x00c9", "É");
            //Ê --> Codigo: 0x00ca
            if (pText.Contains("0x00ca"))
                pText = pText.Replace("0x00ca", "Ê");
            //Ë --> Codigo: 0x00cb
            if (pText.Contains("0x00cb"))
                pText = pText.Replace("0x00cb", "Ë");
            //Ì --> Codigo: 0x00cc
            if (pText.Contains("0x00cc"))
                pText = pText.Replace("0x00cc", "Ì");
            //Í --> Codigo: 0x00cd
            if (pText.Contains("0x00cd"))
                pText = pText.Replace("0x00cd", "Í");
            //Î --> Codigo: 0x00ce
            if (pText.Contains("0x00ce"))
                pText = pText.Replace("0x00ce", "Î");
            //Ï --> Codigo: 0x00cf
            if (pText.Contains("0x00cf"))
                pText = pText.Replace("0x00cf", "Ï");
            //--------------------------------------------------------
            #endregion
            #region d
            //Ð --> Codigo: 0x00d0
            if (pText.Contains("0x00d0"))
                pText = pText.Replace("0x00d0", "Ð");
            //Ñ --> Codigo: 0x00d1
            if (pText.Contains("0x00d1"))
                pText = pText.Replace("0x00d1", "Ñ");
            //Ò --> Codigo: 0x00d2
            if (pText.Contains("0x00d2"))
                pText = pText.Replace("0x00d2", "Ò");
            //Ó --> Codigo: 0x00d3
            if (pText.Contains("0x00d3"))
                pText = pText.Replace("0x00d3", "Ó");
            //Ô --> Codigo: 0x00d4
            if (pText.Contains("0x00d4"))
                pText = pText.Replace("0x00d4", "Ô");
            //Õ --> Codigo: 0x00d5
            if (pText.Contains("0x00d5"))
                pText = pText.Replace("0x00d5", "Õ");
            //Ö --> Codigo: 0x00d6
            if (pText.Contains("0x00d6"))
                pText = pText.Replace("0x00d6", "Ö");
            //× --> Codigo: 0x00d7
            if (pText.Contains("0x00d7"))
                pText = pText.Replace("0x00d7", "×");
            //Ø --> Codigo: 0x00d8
            if (pText.Contains("0x00d8"))
                pText = pText.Replace("0x00d8", "Ø");
            //Ú --> Codigo: 0x00d9
            if (pText.Contains("0x00d9"))
                pText = pText.Replace("0x00d9", "Ú");
            //Û --> Codigo: 0x00da
            if (pText.Contains("0x00da"))
                pText = pText.Replace("0x00da", "Ù");
            //Ü --> Codigo: 0x00db
            if (pText.Contains("0x00db"))
                pText = pText.Replace("0x00db", "Ü");
            //Ý --> Codigo: 0x00dc
            if (pText.Contains("0x00dc"))
                pText = pText.Replace("0x00dc", "Ý");
            //Þ --> Codigo: 0x00de
            if (pText.Contains("0x00de"))
                pText = pText.Replace("0x00de", "Þ");
            //ß --> Codigo: 0x00df
            if (pText.Contains("0x00df"))
                pText = pText.Replace("0x00df", "ß");
            #endregion
            #region e
            //--------------------------------------------------------
            //à --> Codigo: 0x00e0
            if (pText.Contains("0x00e0"))
                pText = pText.Replace("0x00e0", "à");
            //á --> Codigo: 0x00e1
            if (pText.Contains("0x00e1"))
                pText = pText.Replace("0x00e1", "á");
            //â --> Codigo: 0x00e2
            if (pText.Contains("0x00e2"))
                pText = pText.Replace("0x00e2", "â");
            //ã --> Codigo: 0x00e3
            if (pText.Contains("0x00e3"))
                pText = pText.Replace("0x00e3", "ã");
            //ä --> Codigo: 0x00e4
            if (pText.Contains("0x00e4"))
                pText = pText.Replace("0x00e4", "ä");
            //å --> Codigo: 0x00e5
            if (pText.Contains("0x00e5"))
                pText = pText.Replace("0x00e5", "å");
            //æ --> Codigo: 0x00e6
            if (pText.Contains("0x00e6"))
                pText = pText.Replace("0x00e6", "æ");
            //ç --> Codigo: 0x00e7
            if (pText.Contains("0x00e7"))
                pText = pText.Replace("0x00e7", "ç");
            //è --> Codigo: 0x00e8
            if (pText.Contains("0x00e8"))
                pText = pText.Replace("0x00e8", "è");
            //é --> Codigo: 0x00e9
            if (pText.Contains("0x00e9"))
                pText = pText.Replace("0x00e9", "é");
            //ê --> Codigo: 0x00ea
            if (pText.Contains("0x00ea"))
                pText = pText.Replace("0x00ea", "ê");
            //ë --> Codigo: 0x00eb
            if (pText.Contains("0x00eb"))
                pText = pText.Replace("0x00eb", "ë");
            //ì --> Codigo: 0x00ec
            if (pText.Contains("0x00ec"))
                pText = pText.Replace("0x00ec", "ì");
            //í --> Codigo: 0x00ed
            if (pText.Contains("0x00ed"))
                pText = pText.Replace("0x00ed", "í");
            //î --> Codigo: 0x00ee
            if (pText.Contains("0x00ee"))
                pText = pText.Replace("0x00ee", "î");
            //ï --> Codigo: 0x00ef
            if (pText.Contains("0x00ef"))
                pText = pText.Replace("0x00ef", "ï");
            //--------------------------------------------------------
            #endregion
            #region f
            //ð --> Codigo: 0x00f0
            if (pText.Contains("0x00f0"))
                pText = pText.Replace("0x00f0", "ð");
            //ñ --> Codigo: 0x00f1
            if (pText.Contains("0x00f1"))
                pText = pText.Replace("0x00f1", "ñ");
            //ò --> Codigo: 0x00f2
            if (pText.Contains("0x00f2"))
                pText = pText.Replace("0x00f2", "ò");
            //ó --> Codigo: 0x00f3
            if (pText.Contains("0x00f3"))
                pText = pText.Replace("0x00f3", "ó");
            //ô --> Codigo: 0x00f4
            if (pText.Contains("0x00f4"))
                pText = pText.Replace("0x00f4", "ô");
            //õ --> Codigo: 0x00f5
            if (pText.Contains("0x00f5"))
                pText = pText.Replace("0x00f5", "õ");
            //ö --> Codigo: 0x00f6
            if (pText.Contains("0x00f6"))
                pText = pText.Replace("0x00f6", "ö");
            //÷ --> Codigo: 0x00f7
            if (pText.Contains("0x00f7"))
                pText = pText.Replace("0x00f7", "÷");
            //ø --> Codigo: 0x00f8
            if (pText.Contains("0x00f8"))
                pText = pText.Replace("0x00f8", "ø");
            //ù --> Codigo: 0x00f9
            if (pText.Contains("0x00f9"))
                pText = pText.Replace("0x00f9", "ù");
            //ú --> Codigo: 0x00fa
            if (pText.Contains("0x00fa"))
                pText = pText.Replace("0x00fa", "ú");
            //û --> Codigo: 0x00fb
            if (pText.Contains("0x00fb"))
                pText = pText.Replace("0x00fb", "û");
            //ü --> Codigo: 0x00fc
            if (pText.Contains("0x00fc"))
                pText = pText.Replace("0x00fc", "ü");
            //ý --> Codigo: 0x00fd
            if (pText.Contains("0x00fd"))
                pText = pText.Replace("0x00fd", "ý");
            //þ --> Codigo: 0x00fe
            if (pText.Contains("0x00fe"))
                pText = pText.Replace("0x00fe", "þ");
            //ÿ --> Codigo: 0x00ff
            if (pText.Contains("0x00ff"))
                pText = pText.Replace("0x00fd", "ÿ");
            //--------------------------------------------------------
            #endregion
            #region 0x0152 a 0x0192
            //Œ --> Codigo: 0x0152
            if (pText.Contains("0x0152"))
                pText = pText.Replace("0x0152", "Œ");
            //œ --> Codigo: 0x0153
            if (pText.Contains("0x0153"))
                pText = pText.Replace("0x0153", "œ");
            //Š --> Codigo: 0x0160
            if (pText.Contains("0x0160"))
                pText = pText.Replace("0x0160", "Š");
            //š --> Codigo: 0x0161
            if (pText.Contains("0x0161"))
                pText = pText.Replace("0x0161", "š");
            //Ÿ --> Codigo: 0x0178
            if (pText.Contains("0x0178"))
                pText = pText.Replace("0x0178", "Ÿ");
            //ƒ --> Codigo: 0x0192
            if (pText.Contains("0x0192"))
                pText = pText.Replace("0x0192", "ƒ");
            //--------------------------------------------------------
            #endregion
            #region 20
            //– --> Codigo: 0x2013
            if (pText.Contains("0x2013"))
                pText = pText.Replace("0x2013", "–");
            //— --> Codigo: 0x2014
            if (pText.Contains("0x2014"))
                pText = pText.Replace("0x2014", "—");
            //‘ --> Codigo: 0x2018
            if (pText.Contains("0x2018"))
                pText = pText.Replace("0x2018", "‘");
            //’ --> Codigo: 0x2019
            if (pText.Contains("0x2019"))
                pText = pText.Replace("0x2019", "’");
            //‚ --> Codigo: 0x201A
            if (pText.Contains("0x201A"))
                pText = pText.Replace("0x201A", ",");
            //“ --> Codigo: 0x201c
            if (pText.Contains("0x201c"))
                pText = pText.Replace("0x201c", "“");
            //” --> Codigo: 0x201d
            if (pText.Contains("0x201d"))
                pText = pText.Replace("0x201d", "”");
            //„ --> Codigo: 0x00c0
            if (pText.Contains("0x201e"))
                pText = pText.Replace("0x201e", "„");
            //† --> Codigo: 0x2020
            if (pText.Contains("0x2020"))
                pText = pText.Replace("0x2020", "†");
            //‡ --> Codigo: 0x2021
            if (pText.Contains("0x2021"))
                pText = pText.Replace("0x2021", "‡");
            //• --> Codigo: 0x2022
            if (pText.Contains("0x2022"))
                pText = pText.Replace("0x2022", "•");
            //… --> Codigo: 0x2026
            if (pText.Contains("0x2026"))
                pText = pText.Replace("0x2026", "…");
            //‰ --> Codigo: 0x2030
            if (pText.Contains("0x2030"))
                pText = pText.Replace("0x2030", "‰");
            //€ --> Codigo: 0x20ac
            if (pText.Contains("0x20ac"))
                pText = pText.Replace("0x20ac", "€");
            //™ --> Codigo: 0x2122
            if (pText.Contains("0x2122"))
                pText = pText.Replace("0x2122", "™");
            #endregion

           

            return pText;
        }

        

        

        #region Log_NonDatabase

        /// <summary>
        /// intenta almacenar los evcentos en visor de sucesos y/o log.xml
        /// </summary>
        /// <param name="originalEvent">El evento real q se intento loguear</param>
        /// <param name="someError">Error producido por mecanismos de logueo</param>
        public static void Log_NonDatabase(Event originalEvent, Exception someError = null)
        {
            Event someOtherEvent = GetEventFromException(someError);
            try
            {
                //if (WindowsEventFail == false)
                //{
                //    StaticLogger.Log(TargetType.WindowsEvent, originalEvent, string.Empty, string.Empty);
                //    if (someOtherEvent != null)
                //        StaticLogger.Log(TargetType.WindowsEvent, someOtherEvent, string.Empty, string.Empty);
                //}
                //else
                //{
                    Log_FileSystem(originalEvent);
                    Log_FileSystem(someOtherEvent);
                //}
            }
            catch (System.Security.SecurityException ex)
            {
                
                Log_FileSystem(ex);
                Log_FileSystem(someOtherEvent);
                Log_FileSystem(originalEvent);
            }
            //catch (System.ComponentModel.Win32Exception e)//Puede q se llene el log de windows
            //{
            //    WindowsEventFail = true;
            //    Log_FileSystem(e);
            //    Log_FileSystem(someOtherEvent);
            //    Log_FileSystem(originalEvent);
            //}
        }

        /// <summary>
        /// Intenta almacenar eventos en file system
        /// </summary>
        /// <param name="ev"></param>
        public static void Log_FileSystem(Event ev)
        {
            //if (ev == null) return;

            //try
            //{

            //}
            //catch (System.Security.SecurityException ex)
            //{
            //    TechnicalException te = new TechnicalException("No hay permisos para escrivir en el event log", ex);
            //    te.ErrorId = "2000";
            //    te.Source = ex.Source;
            //    throw te;
            //}
        }

        /// <summary>
        /// Intenta almacenar eventos/errores en file system
        /// </summary>
        /// <param name="ev"></param>
        public static void Log_FileSystem(Exception ex)
        {
            if (ex == null) return;

            Event ev = new Event();
            ev.LogType = EventType.Error;
            ev.Source = ex.Source;
            ev.User = Environment.UserName;
            ev.Machine = Environment.MachineName;
            ev.AppId = CommonWorkers.Instance.ServiceName;
            ev.Message = Fwk.Exceptions.ExceptionHelper.GetAllMessageException(ex);
            ev.LogDate = System.DateTime.Now;
            Log_FileSystem(ev);

        }

        static Event GetEventFromException(Exception ex)
        {

            Event ev = new Event();
            ev.LogType = EventType.Error;
            ev.Source = ex.Source;
            ev.User = Environment.UserName;
            ev.Machine = Environment.MachineName;
            ev.AppId = CommonWorkers.Instance.ServiceName;
            ev.Message = Fwk.Exceptions.ExceptionHelper.GetAllMessageException(ex);
            ev.LogDate = System.DateTime.Now;

            return ev;
        }

      

        #endregion

        public static byte[] ConvertStreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pProxy"></param>
        /// <param name="pTimeout"></param>
        public static void HttpPost(Uri pUrl, IWebProxy pProxy, int? pTimeout = null)
        {
            HttpWebRequest wRequest = (HttpWebRequest)WebRequest.Create(pUrl);

            wRequest.KeepAlive = false;

            if (pTimeout.HasValue)
            {
                wRequest.Timeout = pTimeout.Value;
                wRequest.ServicePoint.ConnectionLeaseTimeout = pTimeout.Value;
                wRequest.ServicePoint.MaxIdleTime = pTimeout.Value;
                wRequest.ServicePoint.MaxIdleTime = pTimeout.Value;
                wRequest.ReadWriteTimeout = pTimeout.Value;
            }

            if (pProxy != null)
            {
                wRequest.Proxy = pProxy;
            }

            ASCIIEncoding wEncoding = new ASCIIEncoding();
            Byte[] wByte = wEncoding.GetBytes(pUrl.ToString());

            wRequest.Method = "POST";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.ContentLength = wByte.Length;

            using (Stream wRequestStream = wRequest.GetRequestStream())
            {
                wRequestStream.Write(wByte, 0, wByte.Length);
                wRequestStream.Flush();
                wRequestStream.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pProxy"></param>
        public static StreamReader HttpPost(Uri pUrl, WebProxy pProxy)
        {
            HttpWebRequest wRequest = (HttpWebRequest)WebRequest.Create(pUrl);

            if (pProxy != null)
            {
                wRequest.Proxy = pProxy;
            }

            ASCIIEncoding wEncoding = new ASCIIEncoding();
            Byte[] wByte = wEncoding.GetBytes(pUrl.ToString());

            wRequest.Method = "POST";
            wRequest.ContentType = "application/x-www-form-urlencoded";
            wRequest.ContentLength = wByte.Length;

            Stream wRequestStream = wRequest.GetRequestStream();
            wRequestStream.Write(wByte, 0, wByte.Length);
            wRequestStream.Close();

            return HttpGet(pUrl, pProxy);
        }

        /// <summary>
        /// Se conecta a la URI y te devuelve un reader para leer los resultados de la consulta.
        /// </summary>
        public static StreamReader HttpGet(Uri pUrl, WebProxy pProxy)
        {
            StreamReader wReader = null;

            HttpWebRequest wRequest = (HttpWebRequest)WebRequest.Create(pUrl);

            if (pProxy != null)
            {
                wRequest.Proxy = pProxy;
            }

            HttpWebResponse wResponse = (HttpWebResponse)wRequest.GetResponse();

            wReader = new StreamReader(wResponse.GetResponseStream());


            return wReader;
        }

  

        /// <summary>
        /// Genera una Exception.
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static void GeneratedException(String pMessage, Object pObject, String pSourceName)
        {
            Exception ex = new Exception(pMessage);
            PelsoftException e = PelsoftException.ProcessException(ex, pObject);
            e.Source = pSourceName;
            throw e;
        }

        ///// <summary>
        ///// Método que busca el componente a ejecutar.
        ///// </summary>
        //public static string GetProcessAssembly(AccountsInstancesServicesBE pAccounts, PelsoftEnums.ProcessAssembly pProcessAssembly)
        //{
        //    string wProcessAssembly = ConvertCasesDAC.GetProcessAssemblyByProcessType(pAccounts.AsiConnectionStringDest, Convert.ToInt32(pProcessAssembly));

        //    if (string.IsNullOrEmpty(wProcessAssembly))
        //        Helper.GeneratedException(GetErrorResponse(pAccounts.AsiConnectionStringDest, 8), typeof(Helper), "GetProcessAssembly");

        //    return wProcessAssembly;
        //}

        /// <summary>
        /// Método que ejecuta crear casos o agregar comentarios.
        /// </summary>
        //public static bool RunProcess(AccountsInstancesServicesBE pAccounts, CreateOrCommentParameterBE pCreateOrCommentParameterBE, PelsoftEnums.ProcessAssembly pProcessAssembly)
        //{
        //    IProcessCreateOrComment pIProcessor = (IProcessCreateOrComment)Fwk.HelperFunctions.ReflectionFunctions.CreateInstance(GetProcessAssembly(pAccounts, pProcessAssembly), new Object[] { });

        //    bool wOk = pIProcessor.Process(pAccounts, pCreateOrCommentParameterBE);

        //    return wOk;
        //}

        /// <summary>
        /// Método que ejecuta algún proceso de conversión a casos (TW, FBK, Mail etc.).
        /// </summary>        
        //public static bool RunProcess(AccountsInstancesServicesBE pAccounts, Int32 pCurrentProcessDetailsID, ElementConvertBE pElementConvertBE, PelsoftEnums.ProcessAssembly pProcessAssembly)
        //{
        //    IProcessElementConvert pIProcessor = (IProcessElementConvert)Fwk.HelperFunctions.ReflectionFunctions.CreateInstance(GetProcessAssembly(pAccounts, pProcessAssembly), new Object[] { });

        //    bool wOk = pIProcessor.Process(pAccounts, pCurrentProcessDetailsID, pElementConvertBE);

        //    return wOk;
        //}


        /// <summary>
        /// Método que ejecuta colas de atención.
        /// </summary>
        //public static ProcessAttentionQueueResult RunProcess(ElementAttentionQueueBE pElementAttentionQueueBE, string pProcessAssembly)
        //{
        //    IProcessElementAttentionQueue pIProcessor = (IProcessElementAttentionQueue)Fwk.HelperFunctions.ReflectionFunctions.CreateInstance(pProcessAssembly, new Object[] { });
        //    ProcessAttentionQueueResult wProcessAttentionQueueResult = pIProcessor.Process(pElementAttentionQueueBE);

        //    return wProcessAttentionQueueResult;
        //}

        //EVOLUTION-137 Se agrega clase para agregar parametros. 
        //public static bool RunProcess(ProcessParamsBE processParamsBE)
        //{

        //    IProcessElementConvert pIProcessor = (IProcessElementConvert)Fwk.HelperFunctions.ReflectionFunctions.CreateInstance(GetProcessAssembly(processParamsBE.AccountInfo, processParamsBE.ProccesAssembly), new Object[] { });

        //    bool wOk = pIProcessor.Process(processParamsBE);

        //    return wOk;
        //}

        /// <summary>
        /// Desglosa una Exception en un string de información.
        /// </summary>
        public static string ProcessError(Exception pEx)
        {
            string wProcessError = string.Concat("StackTrace: ", pEx.StackTrace, "; InnerException: ", pEx.InnerException, "; Source: ", pEx.Source, "; Message: ", pEx.Message);

            return wProcessError;
        }

        ///// <summary>
        ///// Método que busca el mensaje de error.
        ///// </summary>
        //public static string GetErrorResponse(string pAsiConnectionStringDest, Int32 pErrorResponseInternalCode)
        //{
        //    return ConvertCasesDAC.GetErrorResponseByInternalCode(pAsiConnectionStringDest, pErrorResponseInternalCode);
        //}

       

        public static string ByteToString(string pAttachmentName, Byte[] pImage)
        {
            string wFileName = string.Empty;

            try
            {
                FileStream wFs;
                string wReturn = string.Empty;
                string wCurrentDirectory = string.Concat(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location).Trim(), "\\Temp");

                string wStrFileName = string.Concat(wCurrentDirectory, "\\", pAttachmentName);

                //Creo el directorio si no existe.
                if (!Directory.Exists(wCurrentDirectory))
                    Directory.CreateDirectory(wCurrentDirectory);

                //Verifico si está el archivo para eliminarlo.
                if (File.Exists(wStrFileName))
                {
                    File.Delete(wStrFileName);
                }

                //Creo el archivo.
                wFileName = wStrFileName;
                wFs = new FileStream(wFileName, FileMode.CreateNew, FileAccess.Write);
                wFs.Write(pImage, 0, pImage.Length);
                wFs.Flush();
                wFs.Close();
            }
            catch
            {
                return wFileName;
            }

            return wFileName;
        }

        public static DateTime UnixTimeToDateTime(Double pUnixTime)
        {
            DateTime vCreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            vCreatedTime = vCreatedTime.AddMilliseconds(pUnixTime).ToLocalTime();

            return vCreatedTime;
        }
        public static DateTime UnixTimeToDateTime(Double pUnixTime, System.DateTimeKind pKind = System.DateTimeKind.Utc)
        {
            DateTime vCreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, pKind);

            vCreatedTime = vCreatedTime.AddMilliseconds(pUnixTime).ToLocalTime();

            return vCreatedTime;
        }

        public static byte[] ImageUrlToArray(string urlImage)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData(urlImage);

            return bytes;
        }

        public static bool IsBase64String(string code)
        {
            code = code.Trim();
            return (code.Length % 4 == 0) && Regex.IsMatch(code, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }

    /// <summary>
    /// Busca la imágen del usuario de una red social en Byte[]
    /// </summary>
    public class WebFunctions
    {
        public static byte[] GetBytesFromUrl(string url, AccountsInstancesServicesBE pAccountServiceInstance)
        {
            byte[] b = null;

            WebResponse myResp = null;
            try
            {
                if (String.IsNullOrEmpty(url))
                    return null;

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);

                //Creo proxy si está configurado
                if (pAccountServiceInstance.Proxy != null)
                {
                    WebProxy myProxy = new WebProxy(pAccountServiceInstance.Proxy.ProxyHost, pAccountServiceInstance.Proxy.ProxyPort);
                    myProxy.Credentials = new System.Net.NetworkCredential(pAccountServiceInstance.Proxy.ProxyUserName, pAccountServiceInstance.Proxy.ProxyPassword, pAccountServiceInstance.Proxy.ProxyDomain);

                    myReq.Proxy = myProxy;
                }
                else
                {
                    myReq.Proxy = null;
                }

                myResp = myReq.GetResponse();

                Stream stream = myResp.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    b = br.ReadBytes(5000000);
                    br.Close();
                }
                myResp.Close();
                return b;
            }
            catch
            {
                return null;
            }
        }

        public static byte[] GetBytesFromUrlNew(string url, AccountsInstancesServicesBE pAccountServiceInstance)
        {
            byte[] wByte = null;
            WebProxy wProxy = null;

            try
            {
                if (String.IsNullOrEmpty(url))
                    return null;

                if (pAccountServiceInstance.Proxy != null)
                {
                    wProxy = new WebProxy(pAccountServiceInstance.Proxy.ProxyHost, pAccountServiceInstance.Proxy.ProxyPort);
                    wProxy.Credentials = new System.Net.NetworkCredential(pAccountServiceInstance.Proxy.ProxyUserName, pAccountServiceInstance.Proxy.ProxyPassword, pAccountServiceInstance.Proxy.ProxyDomain);
                }

                using (WebClient wWebClient = new WebClient())
                {
                    if (wProxy != null)
                        wWebClient.Proxy = wProxy;

                    wByte = wWebClient.DownloadData(url.Trim());
                }

                return wByte;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Busca la imágenes en DM(Twitter) ya que son privadas y hay que ir con las credenciales o sea Tokens.
        /// </summary>
        public static byte[] GetBytesFromTwitterPrivate(string url, string pConsumerKey, string pConsumerSecret, string pAccessToken, string accessTokenSecret, AccountsInstancesServicesBE pAccountServiceInstance)
        {
            byte[] b = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            try
            {
                if (String.IsNullOrEmpty(url))
                    return null;

                WebRequest request = CreateRequest(
                    pConsumerKey, pConsumerSecret, pAccessToken, accessTokenSecret,
                    "GET", url, parameters);

                //Creo proxy si está configurado
                if (pAccountServiceInstance.Proxy != null)
                {
                    WebProxy myProxy = new WebProxy(pAccountServiceInstance.Proxy.ProxyHost, pAccountServiceInstance.Proxy.ProxyPort);
                    myProxy.Credentials = new System.Net.NetworkCredential(pAccountServiceInstance.Proxy.ProxyUserName, pAccountServiceInstance.Proxy.ProxyPassword, pAccountServiceInstance.Proxy.ProxyDomain);

                    request.Proxy = myProxy;
                }
                else
                {
                    request.Proxy = null;
                }

                WebResponse myResp = request.GetResponse();

                Stream stream = myResp.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    b = br.ReadBytes(500000);
                    br.Close();
                }

                myResp.Close();
                return b;
            }
            catch
            {
                return null;
            }

        }

        public static WebRequest CreateRequest(
        string consumerKey, string consumerSecret, string accessToken, string accessKey,
        string method, string url, Dictionary<string, string> parameters, bool isBase64 = false)
        {
            string encodedParams = EncodeParameters(parameters);

            WebRequest request;
            if (method == "GET")
                request = WebRequest.Create(string.Format("{0}?{1}", url, encodedParams));
            else
                request = WebRequest.Create(url);

            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
            if (isBase64)
                request.Headers.Add("Content-Transfer-Encoding", "base64");
            request.Headers.Add(
                "Authorization",
                MakeOAuthHeader(consumerKey, consumerSecret, accessToken, accessKey, method, url, parameters));

            if (method == "POST")
            {
                byte[] postBody = new ASCIIEncoding().GetBytes(encodedParams);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(postBody, 0, postBody.Length);
                }
            }

            return request;
        }


        public static string EncodeLongString(string str)
        {

            //maxLengthAllowed .NET < 4.5 = 32765;
            //maxLengthAllowed .NET >= 4.5 = 65519;
            int maxLengthAllowed = 32765;
            StringBuilder sb = new StringBuilder();
            int loops = str.Length / maxLengthAllowed;

            for (int i = 0; i <= loops; i++)
            {
                sb.Append(Uri.EscapeDataString(i < loops
                    ? str.Substring(maxLengthAllowed * i, maxLengthAllowed)
                    : str.Substring(maxLengthAllowed * i)));
            }

            return sb.ToString();
        }


        public static WebRequest CreateJsonPostRequest(
        string consumerKey, string consumerSecret, string accessToken, string accessKey,
        string url, string body)
        {
            WebRequest request = WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add(
                "Authorization",
                MakeOAuthHeader(consumerKey, consumerSecret, accessToken, accessKey, request.Method, url, null));

            //byte[] postBody = new ASCIIEncoding().GetBytes(body);

            //Se cambia Encoding debido a fallas en la publicacion. Se presentan problemas con los caracteres latinos (¿, letras con tildes, ñ, entre otros)
            //NUNCA SE DEBE CODIFICAR CON ASCIIEncoding cuando haya texto en Español
            byte[] postBody = new UTF8Encoding().GetBytes(body);
            using (Stream stream = request.GetRequestStream())
                stream.Write(postBody, 0, postBody.Length);

            return request;
        }

        static string EncodeParameters(Dictionary<string, string> parameters)
        {
            if (parameters.Count == 0)
                return string.Empty;
            Dictionary<string, string>.KeyCollection.Enumerator keys = parameters.Keys.GetEnumerator();
            keys.MoveNext();
            StringBuilder sb = new StringBuilder(
                string.Format("{0}={1}", keys.Current, EncodeLongString(parameters[keys.Current])));
            while (keys.MoveNext())
                sb.AppendFormat("&{0}={1}", keys.Current, EncodeLongString(parameters[keys.Current]));
            return sb.ToString();
        }

        static string MakeOAuthHeader(string consumerKey, string consumerSecret, string accessToken, string accessKey,
            string method, string url, Dictionary<string, string> parameters)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            string oauth_consumer_key = consumerKey;
            string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            string oauth_signature_method = "HMAC-SHA1";
            string oauth_token = accessToken;
            string oauth_timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            string oauth_version = "1.0";

            SortedDictionary<string, string> sd = new SortedDictionary<string, string>();
            if (parameters != null)
                foreach (string key in parameters.Keys)
                    sd.Add(key, EncodeLongString(parameters[key]));
            sd.Add("oauth_version", oauth_version);
            sd.Add("oauth_consumer_key", oauth_consumer_key);
            sd.Add("oauth_nonce", oauth_nonce);
            sd.Add("oauth_signature_method", oauth_signature_method);
            sd.Add("oauth_timestamp", oauth_timestamp);
            sd.Add("oauth_token", oauth_token);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}&{1}&", method, EncodeLongString(url));
            foreach (KeyValuePair<string, string> entry in sd)
                sb.Append(EncodeLongString(string.Format("{0}={1}&", entry.Key, entry.Value)));
            string baseString = sb.ToString().Substring(0, sb.Length - 3);

            string oauth_token_secret = accessKey;
            string signingKey = string.Format(
                "{0}&{1}", EncodeLongString(consumerSecret), EncodeLongString(oauth_token_secret));
            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));
            string oauth_signature = Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));

            sb = new StringBuilder("OAuth ");
            sb.AppendFormat("oauth_consumer_key=\"{0}\",", EncodeLongString(oauth_consumer_key));
            sb.AppendFormat("oauth_nonce=\"{0}\",", EncodeLongString(oauth_nonce));
            sb.AppendFormat("oauth_signature=\"{0}\",", EncodeLongString(oauth_signature));
            sb.AppendFormat("oauth_signature_method=\"{0}\",", EncodeLongString(oauth_signature_method));
            sb.AppendFormat("oauth_timestamp=\"{0}\",", EncodeLongString(oauth_timestamp));
            sb.AppendFormat("oauth_token=\"{0}\",", EncodeLongString(oauth_token));
            sb.AppendFormat("oauth_version=\"{0}\"", EncodeLongString(oauth_version));

            return sb.ToString();
        }              
    }

    /// <summary>
    /// 
    /// </summary>
    public class EnumerationFunctions
    {
        /// <summary>
        /// Obtiene la descripción de un item de enumeración
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pEnumerationType"></param>
        /// <returns></returns>
        public static string GetDescription<T>(Enum pEnumerationType)
        {
            Type wObjType = typeof(T);
            FieldInfo wFi = wObjType.GetField(pEnumerationType.ToString());
            DescriptionAttribute[] wAttributes = (DescriptionAttribute[])wFi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (wAttributes.Length > 0)
                return wAttributes[0].Description;
            else
                return pEnumerationType.ToString();
        }
    }
}