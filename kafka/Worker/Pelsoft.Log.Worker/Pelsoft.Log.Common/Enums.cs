using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Pelsoft.Log.Common
{
    public class PelsoftEnums
    {
        public enum OriginalType { Account = 1101, Group = 1102 }

        public enum ProcessStatus
        {
            /// <summary>
            /// El proceso está esperando el próximo ciclo de ejecución
            /// </summary>
            WaitingForDoWork,
            /// <summary>
            /// El proceso está detenido al 100%- Pero ahun permanece en la Pila de ///procesos del Engine
            /// </summary>
            Stoped,
            /// <summary>
            /// Se está efectuando tareas dentro de DoWork
            /// En otras palabras el Proceso esta ejecutando tareas Funcionales por que llego un mensaje de la cola
            /// </summary>
            Working
        }

        public enum Channeltype
        {
            #region Facebook
            FacebookFanPage = 301,
            FacebookNotification = 306,
            FacebookFanPageOnDemand = 308,
            FacebookFanPagePublish = 311,
            FacebookExtended = 316,
            FacebookPersonalized = 317,
            FacebookDM = 318,
            FacebookDMPublish = 319,
            FacebookDMPublisOnDemand = 320,
            FacebookScheduler = 322,
            FacebookPersonalizedMark = 328,
            FacebookPersonalizedUser = 329,
            FacebookPhotoComment = 330,
            FacebookStatus = 334,
            FacebookFeed = 336,
            FacebookPromotablePosts = 337,
            FbkLike = 340,
            FacebookPhotoPersonalized = 341,
            FacebookMessengerBot = 356,
            FacebookPublicoBot = 357,
            #endregion

            #region Instagram
            InstagramTags = 342,
            InstagramPublish = 358,
            #endregion

            #region Twitter
            Tweeter = 302,
            TwitterSearch = 305,
            TwitterDM = 307,
            TwitterTimeLineOnDemand = 309,
            TwitterTimeLinePublish = 312,
            TwitterDMOnDemand = 313,
            TwitterDMPublish = 314,
            TwitterScheduler = 321,
            TwitterBack = 331,
            #endregion

            #region Mail
            InboxMail = 303,
            PublishMailOnDemand = 310,
            PublishMail = 315,
            #endregion

            #region Telegram
            Telegram = 338,
            TelegramPublish = 339,
            #endregion

            #region Google & Youtube
            YouTube = 304,
            YoutubePublish = 359,
            GooglePlus = 325,
            GooglePlusPublish = 346,
            #endregion

            #region Mercadolibre
            Meli = 343,
            MeliPublish = 344,
            MeliMessage = 360,
            #endregion

            #region SMS
            SMS = 323,
            SMSPublish = 324,
            SmsOnDemand = 327,
            #endregion

            #region ETL
            ETLMail = 501,
            ETLTwitter = 502,
            ETLFacebook = 503,
            ETLChat = 335,
            #endregion

            #region Creacion de Casos y Asignacion de Casos a Colas
            ConvertCases = 701,
            AttentionQueue = 901,
            #endregion

            #region WhatsApp
            WhatsAppDMPublish = 333,
            WhatsAppTransferConversation = 385,
            WhatsAppOnDemand = 364,
            #endregion

            #region Comerce
            Products = 390,
            Person = 391,
            
            #endregion
        }


        public enum IncidentType
        {
            FatalError = 1,
            Error = 2,
            Warning = 3,
            Information = 4
        }

        #region MailEnums

        public enum TrayEnum
        {
            Inbox = 0, Outbox = 1, Trash = 2, Sended = 3
        }

        public enum ConnectionTypeEnum
        {
            IMAP = 1,
            POP3 = 0,
            EXCHANGE = 2
        }

        public enum ConnectionStatusEnum
        {
            Disconected = 0, // Current User is disconnected
            Connected = 1 // Current User is connected
        }

        public enum AccountTypeEnum
        {
            POP3 = 0,
            IMAP = 1
        }

        #endregion


        public enum AttachmentMediaType
        {
            PHOTO,
            VIDEO,
            LINK
        }

        public enum FacebookType
        {
            Post = 0,
            Comment = 1,
            CommentToComment = 2,
            Album = 3,
            Photo = 4,
            Video = 5,
            PostTagged = 6,
            DarkPost = 700
        }

        public enum InternalCode
        {
            Mail = 1,
            Twitter = 2,
            Facebook = 3,
            SMS = 4,
            Telegram = 5,
            MercadoLibre = 6,
            Mensajero = 7,
            Chat = 8,
            Videollamada = 9,
            ApiChat = 10,
            Instagram = 11,
            Youtube = 12
        }

        public enum ProcessAssembly
        {
            ConverMailToCase = 1,
            ConverFacebookToCase = 2,
            ConverTweetToCase = 3,
            ConverSMSToCase = 4,
            AddComment = 5,
            CreateCase = 6,
            BlockCase = 7,
            ReOpenCase = 8,
            WhiteListQueue = 9,
            ChannelQueue = 10,
            ContentQueue = 11,
            CasesOpenedByConversationThread = 12,
            CasesOpenedByAccount = 13,
            CasesOpenedByGroup = 14,
            AccountQueue = 20,
            ConverMeliToCase = 23,
            ConverMessengerToCase = 24,
            ConverTelegramToCase = 27,
            ConverChatToCase = 29,
            DiayHora = 32,
            ConverVideoToCase = 33,
            ChannelQueueType = 35,
            ConvertApiChatToCase = 36,
            ChannelContentAccount = 38,
            ChannelWhiteListAccount = 39,
            ChannelTypeAccount = 40,
            ByRootIsAccountPost = 41,
            ByRootIsDarkPost = 42,
            ConvertInstagramToCase = 48,
            ConvertYoutubeToCase = 50
        }

        public enum AttentionQueueType
        {
            [Description("Lista Blanca")]
            ByWhiteList = 801,
            [Description("Canal")]
            ByChannel = 803,
            [Description("Cuenta")]
            ByAccount = 804,
            [Description("Default")]
            ByDefault = 809,
            [Description("Contenido")]
            ByContent = 811,
            [Description("PorDiayHora")]
            ByDayAndTime = 807,
            [Description("PorTipodeCanal")]
            ByChannelType = 812,
            [Description("PorContenidoyCuenta")]
            ByContentAccount = 813,
            [Description("PorListaBlancayCuenta")]
            ByWhiteListAccount = 814,
            [Description("Por fuera de la Configuration")]
            ByWithoutConfiguration = 815,
            [Description("Por tipo de canal y cuenta")]
            ByChannelTypeAccount = 816,
            [Description("Por posteo padre es de la marca")]
            ByRootIsAccountPost = 817,
            [Description("Por posteo padre es Dark Post")]
            ByRootIsDarkPost = 818
        }

        public enum MediaType
        {
            Foto = 2,
            Video = 3,
            Link = 4,
            Audio = 5,
            Archivo = 6
        }

        public enum ArchiveType
        {
            jpg = 0,
            mp4 = 1,
            ogg = 2,
            webp = 3
        }

        public enum ObjectType
        {
            Text = 1,
            Combo = 2,
            CheckBox = 3,
            ComboJerarquico = 4,
            Link = 5,
            AtributoValor = 6
        }

        public enum WhatsAppObjectMediaType
        {
            Imagen = 1,
            Audio = 2,
            Documento = 3
        }

    }
}