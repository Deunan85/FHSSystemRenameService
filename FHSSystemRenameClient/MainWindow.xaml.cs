using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;

namespace FHSSystemRenameClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            
        }
    }
    public class DataModel : INotifyPropertyChanged
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
        private ObservableCollection<DataHolder> _computerList;
        #endregion // Data

        #region Accessors
        public ObservableCollection<DataHolder> ComputerList
        {
            get { return _computerList; }
            set
            {
                if (_computerList != value)
                {
                    _computerList = value;
                    OnPropertyChanged("ComputerList");
                }
            }
        }
        #endregion // Accessors
    }

}
