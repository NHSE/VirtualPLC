using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Modbus.Data;
using Modbus.Device;
using VirtualPLC.Models;

namespace VirtualPLC.Interfaces
{
    public interface IPLCManager
    {
        bool bConnect { get; set; }

        event EventHandler<MoterConfig> MotorConfig;
        event EventHandler<MoterConfig> TargetConfig;
        event Action IsConnect;

        Task StartAsync();
        Task Initalize();

    }
}
