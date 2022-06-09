using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using RD2.Crosshairs;
using RD2.Helpers;
using RD2.ViewModel;
using RD2.WinApi;

namespace RD2
{
    /// <summary>
    ///     Interaction logic for Crosshair.xaml
    /// </summary>
    public partial class Crosshair
    {
        private readonly List<Shape> _shapes = new List<Shape>(17);
        private bool bound;
        private UIProcess processToBind;

        public Crosshair()
        {
            this.InitializeComponent();
            this.InitCanvas();
            this.Cursor = Cursors.None;
            this.CrosshairCanvas.Cursor = Cursors.None;
        }

        public Canvas CrosshairCanvas { get; set; }

        public event EventHandler NeedRebind;

        private void InitCanvas()
        {
            var transparentBrush = new SolidColorBrush
            {
                Color = Color.FromArgb(0, 0, 0, 0)
            };

            var canvas = new Canvas
            {
                Background = transparentBrush,
                IsHitTestVisible = false,
                Margin = new Thickness(0, 0, 0, 0)
            };

            this.Content = canvas;
            this.CrosshairCanvas = canvas;
        }

        private void Clean()
        {
            this.CrosshairCanvas.Children.Clear();
            this._shapes.Clear();
        }

        public void DrawCrosshair(CrossHairType type, CrossHairSizeType size, Color color)
        {
            this.Clean();
            var pixelSize = SizingHelper.GetSize(size);
            ICrosshair crosshair;
            switch (type)
            {
                case CrossHairType.Dot:
                    crosshair = new Dot(this._shapes);
                    break;
                case CrossHairType.Cross:
                    crosshair = new Cross(this._shapes);
                    break;
                case CrossHairType.XCross:
                    crosshair = new XCross(this._shapes);
                    break;
                case CrossHairType.RangeFinder:
                    pixelSize.Height *= Rangefinder.HeightFactor;
                    pixelSize.Width *= Rangefinder.WidthFactor;
                    crosshair = new Rangefinder(this._shapes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            this.AdjustSize(pixelSize, crosshair);
            crosshair.Draw(pixelSize, color);
            this.ProcessShapes();
        }

        private void AdjustSize(CrossHairSize pixelSize, ICrosshair crosshair)
        {
            this.CrosshairCanvas.Margin = crosshair.GetPadding(pixelSize);
            this.Width = pixelSize.Width + this.CrosshairCanvas.Margin.Left + this.CrosshairCanvas.Margin.Right;
            this.Height = pixelSize.Height + this.CrosshairCanvas.Margin.Top + this.CrosshairCanvas.Margin.Bottom;
            var coords = ScreenInfo.GetMonitorCenterLocation();
            this.Top = coords.Top - (crosshair.AdjustOnlyLeft ? 0 : this.Height / 2) - this.CrosshairCanvas.Margin.Top;
            this.Left = coords.Left - this.CrosshairCanvas.Margin.Left - this.Width / 2;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwHandle = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwHandle);
        }

        private void ProcessShapes()
        {
            foreach (var shape in this._shapes)
            {
                shape.IsHitTestVisible = false;
                this.CrosshairCanvas.Children.Add(shape);
            }
        }

        public void BindToProcess(UIProcess selectedProcessToBindTo)
        {
            if (selectedProcessToBindTo == null)
            {
                return;
            }

            this.processToBind = selectedProcessToBindTo;
            this.bound = true;
            Action boundCheck = this.BoundCheck;
            this.Dispatcher.InvokeAsync(boundCheck);
        }

        private async void BoundCheck()
        {
            var currentProcess = Process.GetCurrentProcess();
            var thisProcessHandle = currentProcess.MainWindowHandle;
            while (this.bound)
            {
                var activatedHandle = GetForegroundWindow();

                if (activatedHandle != this.processToBind.WindowHandle && activatedHandle != thisProcessHandle)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }

                if (this.processToBind.ProcessObj.HasExited)
                {
                    this.OnNeedRebind();
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }

        public void UnbindFromProcess()
        {
            this.bound = false;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        protected virtual void OnNeedRebind()
        {
            this.NeedRebind?.Invoke(this, EventArgs.Empty);
        }
    }
}