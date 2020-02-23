using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RD2.Crosshairs
{
    internal class Rangefinder : BaseShapesContainer, ICrosshair
    {
        private const double _dotMultiplier = 0.25;

        public Rangefinder(List<Shape> shapes) : base(shapes)
        {
        }
        public bool AdjustOnlyLeft { get; } = true;

        public void Draw(CrossHairSize size, Color color)
        {
            SolidColorBrush colorBrush = new SolidColorBrush
            {
                Color = color
            };

            this.AddDot(size, colorBrush, 0d);
            this.AddDot(size, colorBrush, 0.2d);
            this.AddDot(size, colorBrush, 0.4d);
            this.AddDot(size, colorBrush, 0.6d);
            this.AddDot(size, colorBrush, 0.8d);
            this.AddDot(size, colorBrush, 1d);

            this.AddHorizontalLine(size, colorBrush, 0.2, 1);
            this.AddHorizontalLine(size, colorBrush, 0.4, 2);
            this.AddHorizontalLine(size, colorBrush, 0.6, 3);
            this.AddHorizontalLine(size, colorBrush, 0.8, 4);
            this.AddHorizontalLine(size, colorBrush, 1d, 5);

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

        private void AddDot(CrossHairSize size, SolidColorBrush brush, double percentFromTop)
        {
            Ellipse dot = new Ellipse
            {
                Fill = brush,
                StrokeThickness = 0,
                Width = size.Width * _dotMultiplier,
                Height = size.Width * _dotMultiplier,
                Margin = new Thickness(size.Width * _dotMultiplier * 1.5, size.Height * percentFromTop - size.Width * _dotMultiplier / 2, 0, 0)
            };

            // Set the width and height of the Ellipse.
            this.Shapes.Add(dot);
        }
        private void AddHorizontalLine(CrossHairSize size, SolidColorBrush brush, double percentFromTop, double widthPercent)
        {
            var lineLength = size.Width * widthPercent;
            var verticalLine = new Line
            {
                Stroke = brush,
                StrokeThickness = 1d,
                X1 = size.Width / 2d - lineLength / 2d,
                X2 = size.Width / 2d + lineLength / 2d,
                Y1 = size.Height * percentFromTop,// + size.Width / 4d,
                Y2 = size.Height * percentFromTop// + size.Width / 4d
            };

            // Set the width and height of the Ellipse.
            this.Shapes.Add(verticalLine);
        }
    }
}