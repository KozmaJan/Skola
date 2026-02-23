using Hry.Model;
using Hry.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hry.ViewModel
{
    internal class TileViewModel : ViewModelBase
    {
        public Tile Model { get; }
        public TileViewModel(Tile tile)
        {
            Model = tile;
        }

        // databindingované vlastnosti:

        private bool _revealed;
        public bool Revealed
        {
            get => _revealed;
            set { _revealed = value; OnPropertyChanged(); }
        }
        private int _neighbours;
        public int Neighbours
        {
            get => _neighbours;
            set { _neighbours = value; OnPropertyChanged(); }
        }

        public bool Bomb
        {
            get => Model.bomb;
            set
            {
                if (Model.bomb != value)
                {
                    Model.bomb = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
