using Fwk.Logging;

namespace Pelsoft.Log.Common.Services
{
    public interface IPelsoftLogService
    {
        void Log(PelsoftEvent epironEvent, bool sendMail, AuditMailSetting_Struct auditMailSetting);
        Task LogAsync(PelsoftEvent epironEvent, bool sendMail, AuditMailSetting_Struct auditMailSetting);


        void Log(PelsoftEvent epironEvent, Exception ex, bool sendMail, AuditMailSetting_Struct auditMailSetting);
        Task LogAsync(PelsoftEvent epironEvent, Exception ex, bool sendMail, AuditMailSetting_Struct auditMailSetting);


        void Log(string source, Exception ex, AuditMailSetting_Struct auditMailSetting);
        Task LogAsync(string source, Exception ex, AuditMailSetting_Struct auditMailSetting);


        void Log(string source, Exception ex, bool sendMail);
        Task LogAsync(string source, Exception ex, bool sendMail);


        void Log(string source, string msg, EventType eventType, bool sendMail, AuditMailSetting_Struct auditMailSetting);
        Task LogAsync(string source, string msg, EventType eventType, bool sendMail, AuditMailSetting_Struct auditMailSetting);

        /// <summary>
        /// Use console log
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        void Logger_logError(Exception ex, PelsoftEnums.IncidentType type = PelsoftEnums.IncidentType.Error);

        /// <summary>
        /// Use console log
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        void Logger_logInfo(string msg, PelsoftEnums.IncidentType type = PelsoftEnums.IncidentType.Information);


        void SendMail(string source, string message, AccountsInstancesServicesBE pAccountsInstancesServicesBE);
        void SendMail(PelsoftEvent epironEvent, AuditMailSetting_Struct auditMailSetting);

    }
}