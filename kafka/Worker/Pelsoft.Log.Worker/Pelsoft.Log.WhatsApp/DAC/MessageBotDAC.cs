//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.SqlClient;
//using System.Data.Common;
//using System.Data;

//using System.Threading.Tasks;
//using Pelsoft.Log.Common.Data;
//using Pelsoft.Log.Common;
//using Pelsoft.Log.WhatsApp.BE;

//namespace Pelsoft.Log.WhatsApp.DAC
//{
//    public class MessageBotDAC
//    {

//        public static void Insert(MessageBotBE messageBot)
//        {
//            string wConnectionStr = Helper.GetConnectionString("whatsapp");

//            DBManager.ExecuteStoreProcedure(
//             wConnectionStr,
//             (cnn, cmd) =>
//             {
//                 SqlParameter param;

//                 cmd.CommandText = "[Whatsapp].[MessageBot_i]";

//                 #region params
//                 param = new SqlParameter("@WMessageBotId", SqlDbType.Int);
//                 param.Direction = ParameterDirection.Output;
//                 cmd.Parameters.Add(param);

//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotMessage", messageBot.WMessageBotMessage);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotPhone", messageBot.WMessageBotPhone);
//                 //pidentificador del proveedor.
//                 cmd.Parameters.AddWithValue("@WProviderId", messageBot.WProviderId);
//                 //número de teléfono de la cuenta (movistar,telecom, etc.)
//                 cmd.Parameters.AddWithValue("@WWhatsAppId", messageBot.WWhatsAppId);
//                 //identificador del mensaje
//                 cmd.Parameters.AddWithValue("@WMessageBotExternalId", messageBot.WMessageBotExternalId);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotAttachmentUrl", messageBot.WMessageBotAttachmentUrl);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotTypeAttachment", messageBot.WMessageBotTypeAttachment);
//                 //param type SqlDbType.DateTime
//                 // cmd.Parameters.AddWithValue("@WMessageBotCreatedRow", messageBot.WMessageBotCreatedRow);
//                 //param type SqlDbType.DateTime
//                 cmd.Parameters.AddWithValue("@WMessageBotDeliveredDate", messageBot.WMessageBotDeliveredDate);
//                 messageBot.WMessageBotCreatedDate = messageBot.WMessageBotDeliveredDate;
//                 cmd.Parameters.AddWithValue("@WMessageBotCreatedDate", messageBot.WMessageBotCreatedDate);
//                 //También vimos que el campo WMessageBotProcessDate tiene un valor y este valor tiene que ser NULL, ya que es una fecha que me indica si el mensaje fue traspasado a la Whatsapp.Message
//                 //cmd.Parameters.AddWithValue("@WMessageBotProcessDate", messageBot.WMessageBotProcessDate);
//                 cmd.Parameters.AddWithValue("@WMessageBotReadDate", messageBot.WMessageBotReadDate);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotOrigin", messageBot.WMessageBotOrigin);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotStatus", messageBot.WMessageBotStatus);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotType", messageBot.WMessageBotType);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotParam", messageBot.WMessageBotParam);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotTemplate", messageBot.WMessageBotTemplate);
//                 //param type SqlDbType.VarChar
//                 cmd.Parameters.AddWithValue("@WMessageBotNameSpace", messageBot.WMessageBotNameSpace);
//                 //param type SqlDbType.Bit
//                 cmd.Parameters.AddWithValue("@WMessageBotRedirected", messageBot.WMessageBotRedirected);
//                 #endregion

//                 cmd.ExecuteNonQuery();
//                 messageBot.WMessageBotId = (int)cmd.Parameters["@WMessageBotId"].Value;
//             },
//             (ex) => throw PelsoftException.ProcessException(ex, typeof(WhatsappMessageDAC), wConnectionStr));
//        }
//    }
//}
