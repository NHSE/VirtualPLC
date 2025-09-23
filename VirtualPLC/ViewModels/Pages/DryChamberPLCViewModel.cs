using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualPLC.EnumList;
using VirtualPLC.Interfaces;
using VirtualPLC.Models;

namespace VirtualPLC.ViewModels.Pages
{
    public partial class DryChamberPLCViewModel : ObservableObject
    {
        private readonly IPLCManager _PLCManger;
        private readonly IConfigManager _configManager;

        [ObservableProperty]
        private bool _dryChamber1_motorON = false;
        [ObservableProperty]
        private bool _dryChamber2_motorON = false;
        [ObservableProperty]
        private bool _dryChamber3_motorON = false;
        [ObservableProperty]
        private bool _dryChamber4_motorON = false;
        [ObservableProperty]
        private bool _dryChamber5_motorON = false;
        [ObservableProperty]
        private bool _dryChamber6_motorON = false;

        [ObservableProperty]
        private int _dryChamber1_taget_RPM;
        [ObservableProperty]
        private int _dryChamber2_taget_RPM;
        [ObservableProperty]
        private int _dryChamber3_taget_RPM;
        [ObservableProperty]
        private int _dryChamber4_taget_RPM;
        [ObservableProperty]
        private int _dryChamber5_taget_RPM;
        [ObservableProperty]
        private int _dryChamber6_taget_RPM;

        [ObservableProperty]
        private int _dryChamber1_RPM;
        [ObservableProperty]
        private int _dryChamber2_RPM;
        [ObservableProperty]
        private int _dryChamber3_RPM;
        [ObservableProperty]
        private int _dryChamber4_RPM;
        [ObservableProperty]
        private int _dryChamber5_RPM;
        [ObservableProperty]
        private int _dryChamber6_RPM;


        public DryChamberPLCViewModel(IPLCManager PLCManger, IConfigManager configManager)
        {
            this._configManager = configManager;
            this._PLCManger = PLCManger;

            this._PLCManger.SetConfig(this._configManager.IP, this._configManager.Port);

            _ = this._PLCManger.StartAsync();
            this._PLCManger.MotorConfig += VirtualPLC_GetRPM;
            this._PLCManger.TargetConfig += VirtualPLC_GetTargetRPM;
        }

        private void VirtualPLC_GetRPM(object? sender, MoterConfig e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                switch (e.MoterName)
                {
                    case (int)RegistersEnum.DryChamber1:
                        if (e.MoterRPM == 0)
                        {
                            this.DryChamber1_motorON = false;
                        }

                        this.DryChamber1_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber2:
                        if (e.MoterRPM == 0)
                        {
                            this.DryChamber2_motorON = false;
                        }

                        this.DryChamber2_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber3:
                        if (e.MoterRPM == 0)
                        {
                            this.DryChamber3_motorON = false;
                        }

                        this.DryChamber3_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber4:
                        if (e.MoterRPM == 0)
                        {
                            this.DryChamber4_motorON = false;
                        }

                        this.DryChamber4_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber5:
                        if (e.MoterRPM == 0)
                        {
                            this.DryChamber5_motorON = false;
                        }

                        this.DryChamber5_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber6:
                        if (e.MoterRPM == 0)
                        {
                            this.DryChamber6_motorON = false;
                        }

                        this.DryChamber6_RPM = e.MoterRPM;
                        break;
                }
            });
        }

        private void VirtualPLC_GetTargetRPM(object? sender, MoterConfig e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                switch (e.MoterName)
                {
                    case (int)RegistersEnum.DryChamber1:
                        if (e.MoterRPM != 0)
                        {
                            this.DryChamber1_motorON = true;
                        }

                        this.DryChamber1_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber2:
                        if (e.MoterRPM != 0)
                        {
                            this.DryChamber2_motorON = true;
                        }

                        this.DryChamber2_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber3:
                        if (e.MoterRPM != 0)
                        {
                            this.DryChamber3_motorON = true;
                        }

                        this.DryChamber3_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber4:
                        if (e.MoterRPM != 0)
                        {
                            this.DryChamber4_motorON = true;
                        }

                        this.DryChamber4_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber5:
                        if (e.MoterRPM != 0)
                        {
                            this.DryChamber5_motorON = true;
                        }

                        this.DryChamber5_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.DryChamber6:
                        if (e.MoterRPM != 0)
                        {
                            this.DryChamber6_motorON = true;
                        }

                        this.DryChamber6_taget_RPM = e.MoterRPM;
                        break;
                }
            });
        }
    }
}