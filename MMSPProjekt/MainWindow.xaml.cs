using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MMSPProjekt
{
    public partial class MainWindow : Window
    {
        static ObservableCollection<Grid> grids = new ObservableCollection<Grid>();
        static ObservableCollection<Grid> favs = new ObservableCollection<Grid>();

        public MainWindow()
        {
            InitializeComponent();

            _songs.ItemsSource = grids;
            _favs.ItemsSource = favs;

            addSong("Livin' on a prayer", "Bon Jovi", "4:09", "LivinOnAPrayer.jpg", "https://open.spotify.com/track/37ZJ0p5Jm13JPevGcx4SkF?si=a8dd96c250e94598");
            addSong("Highway to Hell", "AC/DC", "3:28", "HighwayToHell.jpg", "https://open.spotify.com/track/2zYzyRzz6pRmhPzyfMEC8s?si=b66bcededad64edd");
            addSong("Stairway To Heaven", "Led Zeppelin", "8:02", "StairwayToHeaven.jpg", "https://open.spotify.com/track/0DANcJuMamcL9NyYkEWWTq?si=365aaac4577c455d");
            addSong("Heart Shaped Box", "Nirvana", "4:41", "HeartShapedBox.jpg", "https://open.spotify.com/track/11LmqTE2naFULdEP94AUBa?si=5847ccedf371481c");
            addSong("Everlong", "Foo Fighters", "4:10", "Everlong.jpg", "https://open.spotify.com/track/5UWwZ5lm5PKu6eKsHAGxOk?si=24ebaf0ee4d341d7");
            addSong("Under the Bridge", "Red Hot Chilli Peppers", "4:24", "UnderTheBridge.jpg", "https://open.spotify.com/track/3d9DChrdc6BOeFsbrZ3Is0?si=97c9d708d4d24f45");
            addSong("Uptown Girl", "Billy Joel", "3:17", "UptownGirl.jpg", "https://open.spotify.com/track/5zA8vzDGqPl2AzZkEYQGKh?si=ef7fc0abbd20475f");
            addSong("Zamki na piasku", "Lady Pank", "4:30", "ZamkiNaPiasku.jpg", "https://open.spotify.com/track/4F5zFaOhAVkf7sywxrdt8o?si=c78379af0ae847ea");
            addSong("Kołysanka dla nieznajomej", "Perfect", "3:34", "KolysankaDlaNieznajomej.jpg", "https://open.spotify.com/track/4oLgclTNif24XEFC7O7we4?si=97f7a217c1034d31");
            addSong("Za ostatni grosz", "Budka Suflera", "4:50", "ZaOstatniGrosz.jpg", "https://open.spotify.com/track/55dWbx9siuMTNMsIgCtBjl?si=c0b0fcf1eaf3426b");
            addSong("Warszawa", "T.Love", "4:12", "Warszawa.jpg", "https://open.spotify.com/track/5lFmuFUZM6vF36fzjnJZSu?si=6223df9dfd0f49e4");
        }

        private void DodajEl(object sender, RoutedEventArgs e)
        {
            String title = _title.Text;
            String author = _author.Text;
            String length = _length.Text;
            String hyperLink = _hyperLink.Text;
            if (title != "" && author != "" && length != "")
            {
                addSong(title, author, length, "", hyperLink);
                result.Content = "Udało się";
            }
            else
            {
                result.Content = "Uzupełnij Dane";
            }
        }

        public void addSong(String title, String author, String length, String imgSrc = "", String hyperLink = "")
        {
            Piosenka newSong = new Piosenka(title, author, length, imgSrc, hyperLink);
        }

        public class Piosenka
        {
            private String title;
            private String author;
            private String length;
            private String imgSrc;
            private String spotifyLink;
            private bool isFav = false;

            public Piosenka(String title, String author, String length, String imgSrc = "", String hyperLink = "")
            {
                this.title = title;
                this.author = author;
                this.length = length;
                this.imgSrc = imgSrc;
                this.spotifyLink = hyperLink;


                Grid grid = createGrid(title, author, length, imgSrc, spotifyLink);

                Button __add = new Button();
                __add.Style = __add.FindResource("controls") as Style;

                Image __heart = new Image();
                __heart.Style = __heart.FindResource("heart") as Style;

                __add.Content = __heart;
                __add.Click += (sender, e) => addToFav();

                Grid.SetColumn(__add, 2);
                Grid.SetRowSpan(__add, 2);
                grid.Children.Add(__add);

                grids.Add(grid);
            }


            public void addToFav()
            {
                if (!isFav)
                {
                    Grid grid = createGrid(title, author, length, imgSrc, spotifyLink);

                    Button __rem = new Button();
                    __rem.Style = __rem.FindResource("controls") as Style;

                    Image __bin = new Image();
                    __bin.Style = __bin.FindResource("bin") as Style;

                    __rem.Content = __bin;
                    __rem.Click += (sender, e) => remFromFav(grid);

                    Grid.SetColumn(__rem, 2);
                    Grid.SetRowSpan(__rem, 2);
                    grid.Children.Add(__rem);

                    favs.Add(grid);
                    isFav = true;
                }
                else
                {
                    string messageBoxText = "Ta piosenka jest już w polubionych";
                    string caption = "Błąd";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Asterisk;

                    MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
                }
            }
            private void remFromFav(Grid grid)
            {
                favs.Remove(grid);
                isFav = false;
            }

            private Grid createGrid(String title, String author, String length, String imgSrc = "", String spotifyLink = "")
            {
                Grid grid = new() { Width = 420 };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });

                if (imgSrc != "")
                {
                    Image __img = new Image();
                    __img.Style = __img.FindResource("cover") as Style;
                    __img.Source = new BitmapImage(new Uri(@"Images/" + imgSrc, UriKind.Relative));
                    Grid.SetRowSpan(__img, 2);
                    grid.Children.Add(__img);
                }

                TextBlock __author = new TextBlock();
                __author.Text = author;
                __author.Foreground = Brushes.White;
                __author.FontSize = 15;
                __author.Foreground = Brushes.LightGray;

                TextBlock __length = new TextBlock();
                __length.Text = length;
                __length.FontWeight = FontWeights.Light;
                __length.Foreground = Brushes.LightGray;

                Run run = new Run(title);
                Hyperlink __spotifyLink = new Hyperlink(run);
                if (spotifyLink != "")
                    __spotifyLink.NavigateUri = new Uri(spotifyLink);
                __spotifyLink.RequestNavigate += LinkOnRequestNavigate;

                Label __title = new Label();
                __title.Content = __spotifyLink;
                __title.Foreground = Brushes.White;
                __title.FontSize = 18;
                __title.FontWeight = FontWeights.Bold;

                Grid.SetColumn(__title, 1);

                Grid.SetColumn(__author, 1);
                Grid.SetRow(__author, 1);

                Grid.SetColumn(__length, 3);
                Grid.SetRowSpan(__length, 2);

                grid.Children.Add(__title);
                grid.Children.Add(__author);
                grid.Children.Add(__length);

                return grid;
            }
            private void LinkOnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
                e.Handled = true;
            }
        }
    }
}