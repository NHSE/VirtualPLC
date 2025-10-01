using System.ComponentModel;
using System.Runtime.CompilerServices;
using VirtualPLC.EnumList;
using VirtualPLC.Interfaces;
using VirtualPLC.Models;
using VirtualPLC.Services;

namespace VirtualPLC.ViewModels.Pages
{
    public partial class CleanChamberPLCViewModel : ObservableObject
    {
        private readonly IPLCManager _PLCManger;

        [ObservableProperty]
        private bool _cleanChamber1_motorON = false;
        [ObservableProperty]
        private bool _cleanChamber2_motorON = false;
        [ObservableProperty]
        private bool _cleanChamber3_motorON = false;
        [ObservableProperty]
        private bool _cleanChamber4_motorON = false;
        [ObservableProperty]
        private bool _cleanChamber5_motorON = false;
        [ObservableProperty]
        private bool _cleanChamber6_motorON = false;

        [ObservableProperty]
        private int _cleanChamber1_taget_RPM;
        [ObservableProperty]
        private int _cleanChamber2_taget_RPM;
        [ObservableProperty]
        private int _cleanChamber3_taget_RPM;
        [ObservableProperty]
        private int _cleanChamber4_taget_RPM;
        [ObservableProperty]
        private int _cleanChamber5_taget_RPM;
        [ObservableProperty]
        private int _cleanChamber6_taget_RPM;

        [ObservableProperty]
        private int _cleanChamber1_RPM;
        [ObservableProperty]
        private int _cleanChamber2_RPM;
        [ObservableProperty]
        private int _cleanChamber3_RPM;
        [ObservableProperty]
        private int _cleanChamber4_RPM;
        [ObservableProperty]
        private int _cleanChamber5_RPM;
        [ObservableProperty]
        private int _cleanChamber6_RPM;


        public CleanChamberPLCViewModel(IPLCManager PLCManger)
        {
            this._PLCManger = PLCManger;

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
                    case (int)RegistersEnum.CleanChamber1:
                        if(e.MoterRPM == 0)
                        {
                            this.CleanChamber1_motorON = false;
                        }

                        this.CleanChamber1_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber2:
                        if (e.MoterRPM == 0)
                        {
                            this.CleanChamber2_motorON = false;
                        }

                        this.CleanChamber2_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber3:
                        if (e.MoterRPM == 0)
                        {
                            this.CleanChamber3_motorON = false;
                        }

                        this.CleanChamber3_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber4:
                        if (e.MoterRPM == 0)
                        {
                            this.CleanChamber4_motorON = false;
                        }

                        this.CleanChamber4_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber5:
                        if (e.MoterRPM == 0)
                        {
                            this.CleanChamber5_motorON = false;
                        }

                        this.CleanChamber5_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber6:
                        if (e.MoterRPM == 0)
                        {
                            this.CleanChamber6_motorON = false;
                        }

                        this.CleanChamber6_RPM = e.MoterRPM;
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
                    case (int)RegistersEnum.CleanChamber1:
                        if(e.MoterRPM != 0)
                        {
                            this.CleanChamber1_motorON = true;
                        }

                        this.CleanChamber1_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber2:
                        if (e.MoterRPM != 0)
                        {
                            this.CleanChamber2_motorON = true;
                        }

                        this.CleanChamber2_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber3:
                        if (e.MoterRPM != 0)
                        {
                            this.CleanChamber3_motorON = true;
                        }

                        this.CleanChamber3_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber4:
                        if (e.MoterRPM != 0)
                        {
                            this.CleanChamber4_motorON = true;
                        }

                        this.CleanChamber4_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber5:
                        if (e.MoterRPM != 0)
                        {
                            this.CleanChamber5_motorON = true;
                        }

                        this.CleanChamber5_taget_RPM = e.MoterRPM;
                        break;

                    case (int)RegistersEnum.CleanChamber6:
                        if (e.MoterRPM != 0)
                        {
                            this.CleanChamber6_motorON = true;
                        }

                        this.CleanChamber6_taget_RPM = e.MoterRPM;
                        break;
                }
            });
        }
    }
}
