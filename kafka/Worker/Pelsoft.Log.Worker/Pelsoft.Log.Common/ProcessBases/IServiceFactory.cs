using Pelsoft.Log.Common.ProcessBases;

namespace Pelsoft.Log.Common
{
    public interface IServiceFactory
    {
        //void Initialize();
        void LoadProcessFactory();

        Dictionary<Guid, IProcessor> ProcessorDictionary { get; set; }
    }
}