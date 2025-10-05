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
using VirtualPLC.EnumList;

namespace VirtualPLC.Services
{
    public class VirtualPLCService : IPLCManager
    {
        public event EventHandler<MoterConfig> MotorConfig;
        public event EventHandler<MoterConfig> TargetConfig;
        public event Action IsConnect;

        public ModbusTcpSlave slave;
        private TcpListener listener;

        private string IP = "127.0.0.1";
        private int Port = 502;
        public bool bConnect {  get; set; }

        public VirtualPLCService()
        {
            IPAddress ipAddress = IPAddress.Parse(this.IP);
            listener = new TcpListener(ipAddress, Port);

            listener.Start();

            slave = ModbusTcpSlave.CreateTcp(1, listener);
            slave.DataStore = DataStoreFactory.CreateDefaultDataStore();
            _ = slave.ListenAsync();

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(12, 12);
        }

        public async Task Initalize()
        {
            bConnect = false;

            bConnect = await ConnectAsync();

            if (bConnect)
            {
                IsConnect?.Invoke();
                await StartAsync();
            }
        }

        /// <summary>
        /// Slave로 TCP 생성
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ConnectAsync()
        {
            while (true)
            {
                try
                {
                    var Coil = slave.DataStore.CoilDiscretes;
                    if (Coil[(int)RegistersEnum.Master_Connect])
                    {
                        slave.DataStore.CoilDiscretes[(int)RegistersEnum.Slave_Connect] = true;
                        return true;
                    }

                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }
        /// <summary>
        /// Slave로 TCP 생성
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            Console.WriteLine($"Modbus TCP Slave listening on port {Port}");

            const int motorCount = 13;
            Task[] rpmTasks = new Task[motorCount];

            // 12개의 RunRPM Task 실행
            for (int i = 0; i < motorCount; i++)
            {
                int index = i; // 클로저 문제 방지
                rpmTasks[i] = Task.Run(() => RunRPM(index));
            }

            await Task.WhenAll(rpmTasks);

            Console.WriteLine("모든 RunRPM 종료. 재연결 시도...");

            slave.DataStore.CoilDiscretes[(int)RegistersEnum.Slave_Connect] = false;

            bConnect = false;
            IsConnect?.Invoke();

            await Initalize();
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
                    var prevValue = slave.DataStore.InputRegisters;
                    var holdingCoil = slave.DataStore.CoilDiscretes;

                    int targetRpm = holdingRegisters[s]; // 마스터가 쓴 목표 RPM
                    bool flag = holdingCoil[s];

                    if (!holdingCoil[(int)RegistersEnum.Master_Connect])
                    {
                        break;
                    }

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
                        int currentRpm = prevValue[s];
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

                    slave.DataStore.InputRegisters[s] = (ushort)_actualRPM; // 현재 RPM

                    MotorConfig?.Invoke(this, new MoterConfig(s, _actualRPM));

                    await Task.Delay(400);
                }
                catch (Exception ex) // <--- catch 블록 추가
                {
                    Console.WriteLine($"오류 발생: {ex.Message}");
                }

            }
        }
    }
}