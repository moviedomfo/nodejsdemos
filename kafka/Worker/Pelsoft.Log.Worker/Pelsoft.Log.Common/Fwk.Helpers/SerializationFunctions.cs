using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Fwk.HelperFunctions
{
    /// <summary>
    /// Esta clase ayuda con los problemas que tienen que ver
    /// con la serialización de objetos.
    /// </summary>
    public class SerializationFunctions
    {
        

        #region -- Xml Serialization using DataSet --

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObject"></param>
        /// <param name="pDataSet"></param>
        public static void SerializeToXml(object pObject, ref DataSet pDataSet)
        {
            XmlSerializer wSerializer;
            MemoryStream wStream = new MemoryStream();

            if (pDataSet == null)
                pDataSet = new DataSet();

            wSerializer = new XmlSerializer(pObject.GetType());

            wSerializer.Serialize(wStream, pObject);    

            wStream.Position = 0;
            pDataSet.ReadXml(wStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObj"></param>
        /// <param name="pDataSet"></param>
        public static void Serialize(object pObj, ref DataSet pDataSet)
        {
            XmlSerializer serializer;
            MemoryStream ms = new MemoryStream();

            if (pDataSet == null)
                pDataSet = new DataSet();

            serializer = new XmlSerializer(pObj.GetType());
            serializer.Serialize(ms, pObj);

            ms.Position = 0;
            pDataSet.ReadXml(ms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObjType"></param>
        /// <param name="pDataSet"></param>
        /// <param name="pTableName"></param>
        /// <returns></returns>
        public static object Deserialize(Type pObjType, DataSet pDataSet, string pTableName)
        {
            XmlDocument wDom = new XmlDocument();
            wDom.LoadXml(pDataSet.GetXml());
            return Deserialize(pObjType, wDom.GetElementsByTagName(pTableName).Item(0).OuterXml);
        }

        #endregion

        #region -- Xml Serialization using Xml --

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObjType"></param>
        /// <param name="pXmlData"></param>
        /// <returns></returns>
        public static object Deserialize(Type pObjType, string pXmlData)
        {
            XmlSerializer wSerializer;
            UTF8Encoding wEncoder = new UTF8Encoding();
            MemoryStream wStream = new MemoryStream(wEncoder.GetBytes(pXmlData));

            wSerializer = new XmlSerializer(pObjType);
            return wSerializer.Deserialize(wStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pObjType"></param>
        /// <param name="pXmlData"></param>
        /// <param name="pXPath"></param>
        /// <returns></returns>
        public static object Deserialize(Type pObjType, string pXmlData, string pXPath)
        {
            XmlDocument wDom = new XmlDocument();
            wDom.LoadXml(pXmlData);
            return Deserialize(pObjType, wDom.DocumentElement.SelectSingleNode(pXPath).OuterXml);
        }

        /// <summary>
        /// Crea un objeto a partir de la descerializacion del xml pasado por parametro
        /// <code>
        /// var xmlContrato = "<Contrato/>";
        /// 
        /// var contrato = new (ContratoBE)SerializationFunctions.DeserializeFromXml(typeOf(ContratoBE),xmlContrato);
        /// 
        /// </code>
        /// </summary>
        /// <param name="pTipo"></param>
        /// <param name="pXml"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(Type pTipo, string pXml)
        {
            XmlSerializer wSerializer;
            StringReader wStrSerializado = new StringReader(pXml);
            XmlTextReader wXmlReader = new XmlTextReader(wStrSerializado);
            //XmlSerializerNamespaces wNameSpaces = new XmlSerializerNamespaces();
            object wResObj = null;

            //wNameSpaces.Add(String.Empty, String.Empty);
            wSerializer = new XmlSerializer(pTipo);
            wResObj = wSerializer.Deserialize(wXmlReader);

            return wResObj;
        }


        /// <summary>
        /// Serializa un objeto
        /// <code>
        /// var xmlContrato = "<Contrato/>";
        /// Contrato c = new Contrato();
        /// var xmlContrato = SerializationFunctions.SerializeToXml(c);
        /// 
        /// </code>
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static string SerializeToXml(object pObj)
        {
            XmlSerializer wSerializer;
            StringWriter wStwSerializado = new StringWriter();
            XmlTextWriter wXmlWriter = new XmlTextWriter(wStwSerializado);
            XmlSerializerNamespaces wNameSpaces = new XmlSerializerNamespaces();

            wXmlWriter.Formatting = System.Xml.Formatting.Indented;
            wNameSpaces.Add(String.Empty, String.Empty);

            wSerializer = new XmlSerializer(pObj.GetType());
            wSerializer.Serialize(wXmlWriter, pObj, wNameSpaces);


            return wStwSerializado.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", String.Empty);
        }

        /// <summary>
        /// Serializa un objeto.
        /// </summary>
        /// <param name="pObj">Objeto a serializar</param>
        /// <returns>Representación en XML del objeto</returns>
        public static string Serialize(object pObj)
        {
            return Serialize(pObj, false);
        }

        /// <summary>
        /// Serializa un objeto.
        /// </summary>
        /// <param name="pObj">Objeto a serializar</param>
        /// <param name="pRemoveDeclaration">Indica si se debe remover el nodo de declaración</param>
        /// <returns>Representación en XML del objeto</returns>
        public static string Serialize(object pObj, bool pRemoveDeclaration)
        {
            XmlDocument wDoc = new XmlDocument();
            wDoc.Load(GetStream(pObj));

            if (pRemoveDeclaration && wDoc.ChildNodes.Count > 0 && wDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            {
                wDoc.RemoveChild(wDoc.FirstChild);
            }

            return wDoc.InnerXml;
        }


        /// <summary>
        /// Devuelve un stream formado a partir del objeto enviado por parámetro.
        /// </summary>
        /// <param name="pObj">Objeto para extraer stream</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream GetStream(object pObj)
        {
            XmlSerializer wSerializer;
            MemoryStream wStream = new MemoryStream();

            wSerializer = new XmlSerializer(pObj.GetType());
            wSerializer.Serialize(wStream, pObj);

            wStream.Position = 0;

            return wStream;
        }

        #endregion




        /// <summary>
        /// Serializar un objeto utilizando Newtonsoft.Json.SerializeObject
             /// </summary>
        /// <param name="objType">typeOf(type)</param>
        /// <param name="obj">Objetc</param>
        /// <returns></returns>
        public static string SerializeObjectToJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings());
            
            return json;
        }


        /// <summary>
        /// Deserializar un json utilizando Newtonsoft.Json.JsonConvert, requiere casteo de tipos
        /// <code>
        /// Contrato c = (Contrato)SerializationFunctions.DeSerializeObjectFromJson_Newtonsoft(typeOf(Contrato),strContratoJSON);
        /// 
        /// </code>
        /// </summary>
        /// <param name="objType">typeOf(type)</param>
        /// <param name="json">string con formato json</param>
        /// <returns></returns>
        public static object DeSerializeObjectFromJson(Type objType, string json)
        {
            return JsonConvert.DeserializeObject(json, objType, new JsonSerializerSettings());
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTypeNameOld"></param>
        /// <param name="pTypeNameNew"></param>
        /// <param name="pXml"></param>
        /// <returns></returns>
        public static string ReplaseTypeNameForSerialization(Type pTypeNameOld, Type pTypeNameNew, String pXml)
        {
            System.Text.StringBuilder strXml = new System.Text.StringBuilder(pXml);

            strXml.Replace("<" + pTypeNameOld.Name + ">", "<" + pTypeNameNew.Name + ">");
            strXml.Replace("</" + pTypeNameOld.Name + @">", "</" + pTypeNameNew.Name + @">");

            return strXml.ToString();
        }
    }
}