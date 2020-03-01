using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using RD2.Properties;

namespace RD2.ViewModel
{
    public class CrosshairControlViewModel : INotifyPropertyChanged
    {
        private CrossHairSizeType _size = CrossHairSizeType.Normal;
        private CrossHairType _type = CrossHairType.Dot;
        private Color _selectedColor = Colors.Red;
        private bool _started;
        private bool _autoStartAndMinimize;
        public event PropertyChangedEventHandler PropertyChanged;
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CrossHairSizeType Size
        {
            get => this._size;
            set
            {
                if (value == this._size) return;
                this._size = value;
                this.OnPropertyChanged();
            }
        }

        public CrossHairType Type
        {
            get => this._type;
            set
            {
                if (value == this._type) return;
                this._type = value;
                this.OnPropertyChanged();
            }
        }

        public Color SelectedColor
        {
            get => this._selectedColor;
            set
            {
                if (value.Equals(this._selectedColor)) return;
                this._selectedColor = value;
                this.OnPropertyChanged();
            }
        }

        public bool AutoStartAndMinimize
        {
            get => this._autoStartAndMinimize;
            set
            {
                if (value == this._autoStartAndMinimize) return;
                this._autoStartAndMinimize = value;
                this.OnPropertyChanged();
            }
        }

        public bool Started
        {
            get => this._started;
            set
            {
                if (value == this._started) return;
                this._started = value;
                this.OnPropertyChanged();
            }
        }
    }
}