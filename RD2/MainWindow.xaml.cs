using System;
using System.ComponentModel;
using System.Windows;
using RD2.Properties;
using RD2.ViewModel;
using TiqUtils.Serialize;

namespace RD2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Crosshair _activeCrosshair;
        private readonly CrosshairControlViewModel _vm;
        public MainWindow()
        {
            this.InitializeComponent();
            this._vm = this.GetPreviousViewModel();
            this.DataContext = this._vm;
        }

        private CrosshairControlViewModel GetPreviousViewModel()
        {
            var vm = Settings.Default.UserSettings.DeserializeDataFromString<CrosshairControlViewModel>() ?? new CrosshairControlViewModel();
            vm.Started = false;
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
            if (this._activeCrosshair != null) return;

            this._activeCrosshair = new Crosshair();
            this._activeCrosshair.Show();
            this._activeCrosshair.Closed += this.ActiveCrosshairOnClosed;
            this._activeCrosshair.Topmost = true;
            this._vm.Started = true;
            this._activeCrosshair.DrawCrosshair(this._vm.Type, this._vm.Size, this._vm.SelectedColor);
        }

        private void ActiveCrosshairOnClosed(object sender, EventArgs e)
        {
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
                this.Hide();
                this.BarIcon.Visibility = Visibility.Visible;
            }
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
            Settings.Default.UserSettings = this._vm.SerializeDataToString();
            Settings.Default.Save();
            this._activeCrosshair?.Close();
        }
    }
}
