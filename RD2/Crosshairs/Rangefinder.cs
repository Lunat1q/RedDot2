using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using RD2.Helpers;
using RD2.ViewModel;

namespace RD2.Crosshairs
{
    internal class Rangefinder : BaseShapesContainer, ICrosshair
    {
        public const int HeightFactor = 10;
        public const int WidthFactor = 12;

        private const double DotMultiplier = 0.25;
        private readonly CrossHairSize _smallestSize = SizingHelper.GetSize(CrossHairSizeType.Small);

        public Rangefinder(List<Shape> shapes) : base(shapes)
        {
        }

        public bool AdjustOnlyLeft { get; } = true;

        public Thickness GetPadding(CrossHairSize size) => new Thickness(0, this.GetDiameter(size), 0, this.GetDiameter(size));

        public void Draw(CrossHairSize size, Color color)
        {
            var colorBrush = new SolidColorBrush
            {
                Color = color
            };

            var diameter = this.GetDiameter(size);
            this.AddDot(size, diameter, colorBrush, 0d);
            this.AddDot(size, diameter, colorBrush, 0.2d);
            this.AddDot(size, diameter, colorBrush, 0.4d);
            this.AddDot(size, diameter, colorBrush, 0.6d);
            this.AddDot(size, diameter, colorBrush, 0.8d);
            this.AddDot(size, diameter, colorBrush, 1d);

            this.AddHorizontalLine(size, colorBrush, 0.2, 0.2);
            this.AddHorizontalLine(size, colorBrush, 0.4, 0.4);
            this.AddHorizontalLine(size, colorBrush, 0.6, 0.6);
            this.AddHorizontalLine(size, colorBrush, 0.8, 0.8);
            this.AddHorizontalLine(size, colorBrush, 1d, 1d);

            var verticalLine = new Line
            {
                Stroke = colorBrush,
                StrokeThickness = 1d,
                X1 = size.Width / 2d,
                X2 = size.Width / 2d,
                Y1 = 0,
                Y2 = size.Height
            };

            this.Shapes.Add(verticalLine);
        }

        private double GetDiameter(CrossHairSize size)
        {
            return ((size.Width - this._smallestSize.Width) / 10 + this._smallestSize.Width) * DotMultiplier;
        }

        private void AddDot(CrossHairSize size, double diameter, Brush brush, double percentFromTop)
        {
            var dot = new Ellipse
            {
                Fill = brush,
                StrokeThickness = 0,
                Width = diameter,
                Height = diameter,
                Margin = new Thickness((size.Width - diameter) / 2, size.Height * percentFromTop - diameter / 2, 0, 0)
            };

            this.Shapes.Add(dot);
        }

        private void AddHorizontalLine(CrossHairSize size, Brush brush, double percentFromTop, double widthPercent)
        {
            var lineLength = size.Width * widthPercent;
            var verticalLine = new Line
            {
                Stroke = brush,
                StrokeThickness = 1d,
                X1 = (size.Width - lineLength) / 2,
                X2 = (size.Width + lineLength) / 2,
                Y1 = size.Height * percentFromTop,
                Y2 = size.Height * percentFromTop
            };

            this.Shapes.Add(verticalLine);
        }
    }
}