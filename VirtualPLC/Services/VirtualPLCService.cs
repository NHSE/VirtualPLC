using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Modbus.Device;
using Modbus.Data;
using System.Text;
using NModbus;
using VirtualPLC.Models;
using VirtualPLC.Interfaces;

namespace VirtualPLC.Services
{
    public class VirtualPLCService : IPLCManager
    {
        public event EventHandler<MoterConfig> MotorConfig;
        public event EventHandler<MoterConfig> TargetConfig;
        public ModbusTcpSlave slave;
        private string IP = string.Empty;
        private int Port;

        public void SetConfig(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
        }

        /// <summary>
        /// Slave로 TCP 생성
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            IPAddress ipAddress = IPAddress.Parse(this.IP);
            TcpListener listener = new TcpListener(ipAddress, Port);
            listener.Start();
            Console.WriteLine($"Modbus TCP Slave listening on port {Port}");

            slave = ModbusTcpSlave.CreateTcp(1, listener);
            slave.DataStore = DataStoreFactory.CreateDefaultDataStore();
            _ = slave.ListenAsync(); // 백그라운드에서 한번만 실행

            object lockObject = new object();


            for (int i = 2; i < 25; i += 2)
            {
                int s = i;
                _ = RunRPM(s);
            }
        }

        /// <summary>
        /// 모터 당 한개의 쓰레드로 관리하며, 하나의 슬레이브에 여러 모터가 관리됨
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private async Task RunRPM(int s)
        {
            int _actualRPM = 0;
            while (true)
            {
                try
                {
                    var holdingRegisters = slave.DataStore.HoldingRegisters;
                    var holdingCoil = slave.DataStore.CoilDiscretes;

                    int targetRpm = holdingRegisters[s]; // 마스터가 쓴 목표 RPM
                    bool flag = holdingCoil[s];

                    int rpm;

                    if (targetRpm == 0 && !flag)
                    {
                        await Task.Delay(1000); // 딜레이를 넣어 루프가 너무 빠르지 않도록 함
                        continue;
                    }
                    else
                    {
                        rpm = targetRpm;
                        TargetConfig?.Invoke(this, new MoterConfig(s, targetRpm));
                    }

                    int max_random, min_random;
                    if (rpm > 0)
                    {
                        max_random = targetRpm / 10;
                        min_random = targetRpm / 50 < 1 ? 1 : targetRpm / 50;
                    }
                    else
                    {
                        int currentRpm = holdingRegisters[s + 1];
                        max_random = currentRpm / 5 < 3 ? 3 : currentRpm / 5;
                        min_random = currentRpm / 10 < 1 ? 1 : currentRpm / 10;
                    }

                        Random random = new Random();

                    if (_actualRPM < targetRpm)
                    {
                        _actualRPM += random.Next(min_random, max_random);
                        if (_actualRPM > targetRpm) _actualRPM = targetRpm; // 오버런 방지
                    }
                    else if (_actualRPM > targetRpm)
                    {
                        _actualRPM -= random.Next(min_random, max_random);
                        if (_actualRPM < targetRpm) _actualRPM = targetRpm;
                    }
                    else if(_actualRPM == targetRpm && flag)
                    {
                        slave.DataStore.CoilDiscretes[s] = false;
                    }

                    slave.DataStore.HoldingRegisters[s + 1] = (ushort)_actualRPM; // 현재 RPM

                    await Task.Delay(1000);

                    MotorConfig?.Invoke(this, new MoterConfig(s, _actualRPM));
                }
                catch (Exception ex) // <--- catch 블록 추가
                {
                    Console.WriteLine($"오류 발생: {ex.Message}");
                }

            }
        }
    }
}