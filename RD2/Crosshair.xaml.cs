using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using RD2.Crosshairs;

namespace RD2
{
    /// <summary>
    /// Interaction logic for Crosshair.xaml
    /// </summary>
    public partial class Crosshair
    {

        private List<Shape> shapes = new List<Shape>(10);
        public Crosshair()
        {
            this.InitializeComponent();
            this.InitCanvas();
            this.Cursor = Cursors.None;
            this.CrosshairCanvas.Cursor = Cursors.None;
        }

        private void InitCanvas()
        {
            SolidColorBrush transparentBrush = new SolidColorBrush
            {
                Color = Color.FromArgb(0, 0, 0, 0)
            };

            var canvas = new Canvas
            {
                Background = transparentBrush,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                IsHitTestVisible = false
            };

            this.Content = canvas;
            this.CrosshairCanvas = canvas;
        }

        public Canvas CrosshairCanvas { get; set; }

        private void Clean()
        {
            this.CrosshairCanvas.Children.Clear();
            this.shapes.Clear();
        }

        public void DrawCrosshair(CrossHairType type, CrossHairSizeType size, Color color)
        {
            this.Clean();
            var pixelSize = this.GetSize(size);
            ICrosshair crosshair;
            switch (type)
            {
                case CrossHairType.Dot:
                    crosshair = new Dot(this.shapes);
                    break;
                case CrossHairType.Cross:
                    crosshair = new Cross(this.shapes);
                    break;
                case CrossHairType.XCross:
                    crosshair = new XCross(this.shapes);
                    break;
                case CrossHairType.RangeFinder:
                    pixelSize.Height *= 8;
                    pixelSize.Width *= 2;
                    crosshair = new Rangefinder(this.shapes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            this.AdjustSize(pixelSize, 2, crosshair.AdjustOnlyLeft);
            crosshair.Draw(pixelSize, color);
            this.ProcessShapes();
        }
        
        private void AdjustSize(CrossHairSize pixelSize, double multiplier, bool leftOnly = false)
        {
            this.Width = pixelSize.Width * multiplier;
            this.Height = pixelSize.Height * multiplier;
            var coords = ScreenInfo.GetCenterOfCoordinates(pixelSize, leftOnly);
            this.Top = coords.Top;
            this.Left = coords.Left;

            this.CrosshairCanvas.Margin = new Thickness(-1d * pixelSize.Width, leftOnly ? 0 : - 1d * pixelSize.Height, 0, 0);
        }

        private CrossHairSize GetSize(CrossHairSizeType size)
        {
            switch (size)
            {
                case CrossHairSizeType.Small:
                    return new CrossHairSize(5, 5);
                case CrossHairSizeType.Normal:
                    return new CrossHairSize(10, 10);
                case CrossHairSizeType.Big:
                    return new CrossHairSize(20, 20);
                case CrossHairSizeType.Huge:
                    return new CrossHairSize(40, 40);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwHandle = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwHandle);
        }
        
        private void ProcessShapes()
        {
            foreach (var shape in this.shapes)
            {
                shape.IsHitTestVisible = false;
                this.CrosshairCanvas.Children.Add(shape);
            }
        }
    }

    public enum CrossHairType
    {
        Dot,
        Cross,
        XCross,
        RangeFinder
    }
}
