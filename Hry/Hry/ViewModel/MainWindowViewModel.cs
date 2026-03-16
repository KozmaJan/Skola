using Hry.Model;
using Hry.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hry.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand StartCommand => new RelayCommand(execute => StartGame(), canExecute => _isGameRunning == false);
        public RelayCommand TileClickCommand => new RelayCommand(execute => TileClicked(execute as TileViewModel), canExecute => _isGameRunning == true);

        public MainWindowViewModel()
        {
            Tiles = new ObservableCollection<TileViewModel>();
        }

        private Color _defaultColor = Colors.AliceBlue;
        private Color _higlightColor = Colors.Green;

        private bool _isGameRunning = false;
        private bool _isBusy = false;
        private bool _generated = false;

        private TileViewModel Selected;


        private int _height = 8;
        public int height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _width = 8;
        public int width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _bombs = 16;
        public int bombs
        {
            get => _bombs;
            set
            {
                if (_bombs != value)
                {
                    _bombs = value;
                    OnPropertyChanged();
                }
            }
        }

        int[] dirs; //směry, pomocí kterých čekuje, kde se mini nachází a kde ne

        #region Data Binding

        // Vlastnosti, na nichž máme data binding: karty pexesa, velikost gridu (neměnné), skóre
        public ObservableCollection<TileViewModel> Tiles { get; set; }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Herní logika
        // Samotná herní logika
        public void StartGame()
        {
            Tiles.Clear();
            Score = 0;
            firstClick = true;
            CreateField();
            //OnPropertyChanged(nameof(GridSize)); // máme nachystané karty, vyvoláme funkci, že se grid změnil
            _isGameRunning = true;
        }
        private void CreateField()
        {
            // přidáme dvojice karet
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tiles.Add(new TileViewModel(new Tile(j, i, false)));
                }
            }
            dirs = new int[] {-width - 1, -width, -width+1, -1, 1, width - 1, width, width + 1 };
        }

        private void PlaceMines(int fpos)//položí miny až po kliknutí na první políčku
        {//argument pos je zde pro určení prvního políčka, jenž bude vynecháno z generace min
            Random rng = new Random();
            for (int i = 0; i < bombs; i++)
            {
                int x = rng.Next(0, width);
                int y = rng.Next(0, height);
                int pos = width * y + x;

                if (Tiles[pos].Model.bomb == true || pos == fpos)
                {
                    i--;
                    continue;
                }

                Tiles[pos].Model.bomb = true;
            }
        }

        private bool markMode;
        private bool firstClick;
        private async void TileClicked(TileViewModel clicked)
        {

            if (_isBusy) return; // probíhá čekání u 2 ukázaných karet

            if (clicked.Revealed) return;

            int pos = clicked.Model.y * width + clicked.Model.x;

            if (firstClick)
            {
                PlaceMines(pos);
                firstClick = false;
            }

            clicked.Revealed = true;

            if(clicked.Model.bomb == true)
            {
                _isGameRunning = false;
                RevealBombs();
                MessageBox.Show("Bum!", "Chainsawman Kobeni reference?", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //_isBusy = true;
            await Task.Delay(1);


            // zkontrolujem, jestli je políčko mina a jestli máme markmode
            if (clicked.Model.bomb == false && !markMode)
            {
                clicked.Neighbours = FindNeigbours(pos);
                Score++;
            }
            else if (markMode) // implementace později
            {
                
            }

            _isBusy = false; // konec čekání

            if (Score == width*height - bombs)
            {
                _isGameRunning = false;
                RevealBombs();
                MessageBox.Show("Výhra.", "Good job", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
               

        }
        public int FindNeigbours(int pos) {//pomocí dirs, zkontroluje jestli se bomby nachází v sousedních políčkách
            int neigbours = 0;
            if(pos % width == 0)
                dirs = new int[] {-width, -width + 1, 1, width, width + 1 };
            else if (pos % width == width - 1)
                dirs = new int[] { -width - 1, -width, -1, width - 1, width};
            else
                dirs = new int[] { -width - 1, -width, -width + 1, -1, 1, width - 1, width, width + 1 };

            foreach (int dir in dirs)
            {
                if (pos + dir >= 0 && pos + dir < Tiles.Count )
                {
                    if(Tiles[pos + dir].Model.bomb == true)//pokud je v rozahu tabulky
                    {
                        neigbours++;
                    }
                }
            }
            return neigbours;
        }
        void RevealBombs()
        {
            int i = 0;
            foreach(TileViewModel tile in Tiles)
            {
                tile.Neighbours = FindNeigbours(i);
                tile.Revealed = true;
                if (tile.Model.bomb)
                    tile.Revealed = true;
                i++;
            }
        }


        #endregion

    }

}



