using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFSMonitor.Models;

namespace VFSMonitor.Interfaces
{
    public interface IMonitorApiGetSessions
    {
        [Get("/sessions")]
        Task<List<Session>> GetSessions();
    }
}
