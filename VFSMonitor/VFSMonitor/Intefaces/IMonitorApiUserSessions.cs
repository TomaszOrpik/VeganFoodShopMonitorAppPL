using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFSMonitor.Models;

namespace VFSMonitor.Intefaces
{
    public interface IMonitorApiUserSessions
    {
        [Get("/sessions_user/{id}")]
        Task<List<Session>> GetUserSessions([AliasAs("id")] string userId);
    }
}
