using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MMSPProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Object> songs = new ObservableCollection<Object>();
        static ObservableCollection<Button> bttns = new ObservableCollection<Button>();
        static ObservableCollection<Button> favs = new ObservableCollection<Button>();
        public MainWindow()
        {
            InitializeComponent();
            
            _songs.ItemsSource = bttns;
            _favs.ItemsSource = favs;

            addSong("Livin on a prayer", "Bon Jovi", "3:30");
            addSong("Livin on a prayer", "Bon Jovi", "3:30");
            addSong("Livin on a prayer", "Bon Jovi", "3:30");
        }


        private void DodajEl(object sender, RoutedEventArgs e)
        {
            String title = _title.Text;
            String author = _author.Text;
            String length = _length.Text;
            if (title != "" && author != "" && length != "")
            {
                addSong(title, author, length);
                _formHeader.Text = "Udało się";
            }
            else
            {
                _formHeader.Text = "Wystąpił błąd";
            }
        }
        
        public void addSong(String title, String author, String length)
        {
            Piosenka newSong = new Piosenka(title, author, length);
            songs.Add(newSong);
        }

        public class Piosenka
        {
            public String title;
            public String author;
            public String length;
            //private int songID;
           

            public Piosenka(String title, String author, String length)
            {
                this.title = title;
                this.author = author;
                this.length = length;
                //this.songID = songID;
               

                Button button1 = new Button();
                button1.Content = title + "|\t" + author + "|\t" + length;
                button1.Width = 300;
                button1.Height = 50;
                button1.HorizontalAlignment = HorizontalAlignment.Left;

                button1.Click += (sender, e) => addToFav(button1);
                MainWindow.bttns.Add(button1);

                bttns.Add(button1);

            }

            public void addToFav(Button button1)
            {
                Button button2 = new Button();
                button2.Content = title + "|\t" + author + "|\t" + length;
                button2.Width = 300;
                button2.Height = 50;
                button2.Click += (sender, e) => remFromFav(button2);
                favs.Add(button2);
            }
            public void remFromFav(Button button2)
            {
                favs.Remove(button2);
            }
        }
    }
}