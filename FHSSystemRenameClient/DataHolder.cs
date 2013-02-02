using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FHSSystemRenameClient
{
    public class DataHolder : INotifyPropertyChanged
    {
        #region INotifyProperyEvent
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion // INotifyPropertyEvent

        #region Data
        private string _IPAddress;
        private string _ComputerName;
        private bool _ToRename;
        private bool _Renamed;
        #endregion // Data
        public string IPAddress
        {
            get { return _IPAddress; }
            set
            {
                if (_IPAddress != value)
                {
                    _IPAddress = value;
                    OnPropertyChanged("IPAddress");
                }
            }
        }
        public string ComputerName
        {
            get { return _ComputerName; }
            set
            {
                if (_ComputerName != value)
                {
                    _ComputerName = value;
                    OnPropertyChanged("ComputerName");
                }
            }
        }
        public bool ToRename
        {
            get { return _ToRename; }
            set
            {
                if (_ToRename != value)
                {
                    _ToRename = value;
                    OnPropertyChanged("ToRename");
                }
            }
        }
        public bool Renamed
        {
            get { return _Renamed; }
            set
            {
                if (_Renamed != value)
                {
                    _Renamed = value;
                    OnPropertyChanged("Renamed");
                }
            }
        }
        #region Accessors

        #endregion // Accessors

        public DataHolder(string IPAddress, string ComputerName, bool Rename)
        {
            this.IPAddress = IPAddress;
            this.ComputerName = ComputerName;
            this.ToRename = Rename;
            this.Renamed = false;
        }
    }
}
