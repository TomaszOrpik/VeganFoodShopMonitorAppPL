using Refit;
using System.Threading.Tasks;
using VFSMonitor.Models;

namespace VFSMonitor.Intefaces
{
    public interface IMonitorApiGlobal
    {
        [Get("/users_average")]
        Task<GlobalAverageData> GetGlobal();
    }
}
