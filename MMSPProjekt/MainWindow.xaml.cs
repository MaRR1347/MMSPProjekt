using System.Collections.ObjectModel;
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

namespace MMSPProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<String> songs = new ObservableCollection<String>();
        ObservableCollection<String> list = new ObservableCollection<String>();
        public MainWindow()
        {
            InitializeComponent();
        

            _ListView.ItemsSource = list;

            list.Add("abecadło");
            list.Add("z nieba spadło");
        
        
        }
        private void DodajEl(object sender, RoutedEventArgs e)
        {
            list.Add("Przycisk");
        }

        
    }
}