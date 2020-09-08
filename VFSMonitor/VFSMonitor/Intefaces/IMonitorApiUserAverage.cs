using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFSMonitor.Models;

namespace VFSMonitor.Intefaces
{
    public interface IMonitorApiUserAverage
    {
        [Get("/users_average/{id}")]
        Task<UserAverageData> GetUserAverage([AliasAs("id")] string userId);
    }
}
