using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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


            addSong("Livin' on a prayer", "Bon Jovi", "4:09", "LivinOnAPrayer.jpg");
            addSong("Highway to Hell", "AC/DC", "3:28", "HighwayToHell.jpg");
            addSong("Stairway To Heaven", "Led Zeppelin", "8:02", "StairwayToHeaven.jpg");
            addSong("Heart Shaped Box", "Nirvana", "4:41", "HeartShapedBox.jpg");
            addSong("Everlong", "Foo Fighters", "4:10", "Everlong.jpg");
            addSong("Under the Bridge", "Red Hot Chilli Peppers", "4:24", "UnderTheBridge.jpg");
            addSong("Uptown Girl", "Billy Joel", "3:17", "UptownGirl.jpg");
            addSong("Zamki na piasku", "Lady Pank", "4:30", "ZamkiNaPiasku.jpg");
            addSong("Kołysanka dla nieznajomej", "Perfect", "3:34", "KolysankaDlaNieznajomej.jpg");
            addSong("Za ostatni grosz", "Budka Suflera", "4:50", "ZaOstatniGrosz.jpg");
            addSong("Warszawa", "T.Love", "4:12", "Warszawa.jpg");
        }

        //S
        private void DodajEl(object sender, RoutedEventArgs e)
        {
            String title = _title.Text;
            String author = _author.Text;
            String length = _length.Text;
            if (title != "" && author != "" && length != "")
            {
                addSong(title, author, length);
                result.Content = "Udało się";
            }
            else
            {
                result.Content = "Uzupełnij Dane";
            }
        }


        public void addSong(String title, String author, String length, String imgSrc = "")
        {
            Piosenka newSong = new Piosenka(title, author, length, imgSrc);
        }

        public class Piosenka
        {
            private String title;
            private String author;
            private String length;
            private String imgSrc;
            private bool isFav = false;

            public Piosenka(String title, String author, String length, String imgSrc = "")
            {
                this.title = title;
                this.author = author;
                this.length = length;
                this.imgSrc = imgSrc;


                Grid grid = createGrid(title, author, length, imgSrc);

                Button __add = new Button();
                __add.Style = __add.FindResource("controls") as Style;
                
                Image __heart = new Image();
                __heart.Style = __heart.FindResource("heart") as Style;

                //__heart.Source = new BitmapImage(new Uri(@"Images/heart_full.png", UriKind.Relative

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
                    Grid grid = createGrid(title, author, length, imgSrc);

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

            private Grid createGrid(String title, String author, String length, String imgSrc)
            {
                Grid grid = new Grid();
                grid.Width = 420;
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

                TextBlock __title = new TextBlock();
                __title.Text = title;
                __title.Foreground = Brushes.White;
                __title.FontSize = 18;
                __title.FontWeight = FontWeights.Bold;

                TextBlock __author = new TextBlock();
                __author.Text = author;
                __author.Foreground = Brushes.White;
                __author.FontSize = 15;
                __author.Foreground = Brushes.LightGray;

                TextBlock __length = new TextBlock();
                __length.Text = length;
                __length.FontWeight = FontWeights.Light;
                __length.Foreground = Brushes.LightGray;


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
        }
    }
}