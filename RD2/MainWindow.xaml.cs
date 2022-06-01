using System;
using System.ComponentModel;
using System.Windows;
using RD2.Properties;
using RD2.ViewModel;
using TiqUtils.Serialize;

namespace RD2
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly CrosshairControlViewModel _vm;
        private Crosshair _activeCrosshair;
        private CrossHairSettings _settings;

        public MainWindow()
        {
            this._vm = this.GetViewModelWithSettingsRestored();
            this.InitializeComponent();
            if (this._vm.AutoStartAndMinimize)
            {
                this.Start_Click(null, null);
                this.Minimize();
            }

            this.DataContext = this._vm;
        }

        private CrosshairControlViewModel GetViewModelWithSettingsRestored()
        {
            var vm = new CrosshairControlViewModel();

            this._settings = Settings.Default.UserSettings.DeserializeDataFromString<CrossHairSettings>() ?? new CrossHairSettings();
            vm.RefreshProcessList(this._settings);
            vm.RestoreSettings(this._settings);
            vm.PropertyChanged += this.VmOnPropertyChanged;
            return vm;
        }

        private void VmOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this._vm.Started)
            {
                this._activeCrosshair?.DrawCrosshair(this._vm.Type, this._vm.Size, this._vm.SelectedColor);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeCrosshair != null)
            {
                return;
            }

            this._activeCrosshair = new Crosshair();
            this._activeCrosshair.Focusable = false;
            this._activeCrosshair.ShowActivated = false;
            //this._activeCrosshair.WindowState = WindowState.Maximized;
            this._activeCrosshair.Owner = this;
            this._activeCrosshair.Show();
            this._activeCrosshair.Closed += this.ActiveCrosshairOnClosed;
            this._activeCrosshair.Topmost = true;
            this._vm.Started = true;
            this._activeCrosshair.DrawCrosshair(this._vm.Type, this._vm.Size, this._vm.SelectedColor);
            if (this._vm.BoundToProcess)
            {
                this._activeCrosshair.BindToProcess(this._vm.SelectedProcess);
            }

        }

        private void ActiveCrosshairOnClosed(object sender, EventArgs e)
        {
            this._activeCrosshair.UnbindFromProcess();
            this._activeCrosshair.Closed -= this.ActiveCrosshairOnClosed;
            this._activeCrosshair = null;
            this._vm.Started = false;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this._activeCrosshair?.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Minimize();
            }
        }

        private void Minimize()
        {
            this.Hide();
            this.BarIcon.Visibility = Visibility.Visible;
        }

        private void ShowWindow(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this._activeCrosshair?.Close();
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.UserSettings = this._vm.GetSettings().SerializeDataToString();
            Settings.Default.Save();
            this._activeCrosshair?.Close();
        }

        private void BindToProcess_Click(object sender, RoutedEventArgs e)
        {
            this._vm.BoundToProcess = !this._vm.BoundToProcess;
            if (this._vm.BoundToProcess)
            {
                this._activeCrosshair?.BindToProcess(this._vm.SelectedProcess);
            }
            else
            {
                this._activeCrosshair?.UnbindFromProcess();
            }
        }

        private void RefreshProcess_Click(object sender, RoutedEventArgs e)
        {
            this._vm.RefreshProcessList(this._settings);
        }
    }
}