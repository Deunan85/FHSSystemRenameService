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
using System.ServiceModel;

namespace FHSSystemRenameClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataModel dm;
        public MainWindow()
        {
            this.DataContext = new DataModel();
            dm = (DataModel)DataContext;
            InitializeComponent();
        }

        private void btOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Computer List";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Comma Separated Values (.csv)|*.csv";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box
            if (result == true)
            {
                tbFileName.Text = dlg.SafeFileName;
                dm.ComputerList = ObservableCollectionBuilder.Parse(dlg.FileName);
            }
        }

        private void btRenameComputer_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataHolder item in dm.ComputerList)
            {
                // Create the endpoint to connect to
                string endPointAddr = "http://" + item.IPAddress + ":8080/SystemRenameService";
                EndpointAddress endPointAddress = new EndpointAddress(endPointAddr);
                WSHttpBinding binding = new WSHttpBinding();
                using (SystemRenameService.SystemRenameServiceClient client = new SystemRenameService.SystemRenameServiceClient(binding, endPointAddress))
                {
                    // Send rename command
                    item.Renamed = client.RenameComputer(item.ComputerName);
                }
                // Close connection
            }
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
