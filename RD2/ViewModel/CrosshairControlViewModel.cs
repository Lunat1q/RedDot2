using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using RD2.Properties;

namespace RD2.ViewModel
{
    public class CrosshairControlViewModel : INotifyPropertyChanged
    {
        private bool _autoStartAndMinimize;
        private bool _boundToProcess;
        private Color _selectedColor = Colors.Red;
        private CrossHairSizeType _size = CrossHairSizeType.Normal;
        private bool _started;
        private CrossHairType _type = CrossHairType.Dot;
        private UIProcess _selectedProcess;

        public CrossHairSizeType Size
        {
            get => this._size;
            set
            {
                if (value == this._size)
                {
                    return;
                }

                this._size = value;
                this.OnPropertyChanged();
            }
        }

        public CrossHairType Type
        {
            get => this._type;
            set
            {
                if (value == this._type)
                {
                    return;
                }

                this._type = value;
                this.OnPropertyChanged();
            }
        }

        public Color SelectedColor
        {
            get => this._selectedColor;
            set
            {
                if (value.Equals(this._selectedColor))
                {
                    return;
                }

                this._selectedColor = value;
                this.OnPropertyChanged();
            }
        }

        public bool AutoStartAndMinimize
        {
            get => this._autoStartAndMinimize;
            set
            {
                if (value == this._autoStartAndMinimize)
                {
                    return;
                }

                this._autoStartAndMinimize = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<UIProcess> UIProcesses { get; set; } = new ObservableCollection<UIProcess>();

        public UIProcess SelectedProcess
        {
            get => this._selectedProcess;
            set
            {
                if (value == this._selectedProcess)
                {
                    return;
                }
                this._selectedProcess = value;
                this.OnPropertyChanged();
            }
        }

        public bool Started
        {
            get => this._started;
            set
            {
                if (value == this._started)
                {
                    return;
                }

                this._started = value;
                this.OnPropertyChanged();
            }
        }

        public bool BoundToProcess
        {
            get => this._boundToProcess;
            set
            {
                if (value == this._boundToProcess)
                {
                    return;
                }

                this._boundToProcess = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProcessList(CrossHairSettings settings)
        {
            var processList = Process.GetProcesses();
            this.UIProcesses.Clear();
            this.SelectedProcess = null;
            foreach (var process in processList.Where(proc => proc.MainWindowHandle != IntPtr.Zero))
            {
                var uiProcess = new UIProcess
                {
                    Name = process.ProcessName,
                    PID = process.Id,
                    WindowHandle = process.MainWindowHandle,
                    Title = string.IsNullOrWhiteSpace(process.MainWindowTitle) ? process.ProcessName : process.MainWindowTitle,
                    ProcessObj = process
                };
                this.UIProcesses.Add(uiProcess);
                if (process.ProcessName == settings.SelectedProcessName)
                {
                    this.SelectedProcess = uiProcess;
                }
            }
        }

        public void RestoreSettings(CrossHairSettings settings)
        {
            this.Size = settings.Size;
            this.Type = settings.Type;
            this.SelectedColor = settings.SelectedColor;
            this.AutoStartAndMinimize = settings.AutoStartAndMinimize;
            this.BoundToProcess = settings.BoundToProcess;
        }

        public CrossHairSettings GetSettings()
        {
            return new CrossHairSettings
            {
                SelectedProcessName = this.SelectedProcess?.Name,
                Size = this.Size,
                Type = this.Type,
                SelectedColor = this.SelectedColor,
                AutoStartAndMinimize = this.AutoStartAndMinimize,
                BoundToProcess = this.BoundToProcess
            };
        }
    }
}