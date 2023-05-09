using Fwk.Logging;

namespace Pelsoft.Log.Common.ProcessBases
{
    public interface IProcessor
    {
        DateTime Sleep();
        //void CreateNewProcess();
        //void CloseProcessDetails();
        event IncidentHandler IncidentEvent;
        Guid ProcessorId { get; set; }
        AccountsInstancesServicesBE AccountInfo { get; set; }
        System.Net.WebProxy Proxy { get; set; }
        string SourceName { get; set; }
        PelsoftEnums.ProcessStatus ProcessStatus { get; set; }
        DateTime RefreshActiveDate { get; set; }

        /// <summary>
        /// Do any business job .-
        /// </summary>
        /// <param name="message">Menssage or value from queue</param>
        /// <param name="topic">Spesific Queue name, This is so since the process listens to more than one queue or topic. </param>
        /// <returns></returns>
        Task DoWork(string message, string topic);
        //Task<int> DoTask();
        //bool CheckStopTime();
        //Task StartAsync();
        void Start();
        void Stop();
        void Restart();
        //void Log(string msg, EventType eventType, bool sendMail);
        //void Log(Exception ex);

        void RefreshProcessData();

        //void Set_AuditMailSetting();

        

    }
}
