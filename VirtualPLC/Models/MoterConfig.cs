using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPLC.Models
{
    public partial class MoterConfig : ObservableObject
    {
        [ObservableProperty]
        private int _moterName;

        [ObservableProperty]
        private int _moterRPM;

        public MoterConfig(int value1, int value2)
        {
            _moterName = value1;
            _moterRPM = value2;
        }
    }
}
