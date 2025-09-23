using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPLC.Interfaces
{
    public partial interface IConfigManager
    {
        #region PROPERTIES
        string IP { get; set; }
        int Port { get; set; }
        #endregion

        #region METHODS
        void InitConfig();

        void UpdateConfigValue(string key, string newValue);

        string GetFilePathAndCreateIfNotExists();
        #endregion

        #region EVENTS
        event Action ConfigRead;
        #endregion
    }
}
