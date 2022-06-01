using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using RD2.ViewModel;

namespace RD2.Crosshairs
{
    internal class Cross : BaseShapesContainer, ICrosshair
    {
        public Cross(List<Shape> shapes) : base(shapes)
        {
        }
        public bool AdjustOnlyLeft { get; } = false;

        public Thickness GetPadding(CrossHairSize size) => new Thickness(0, 0, 0, 0);
        public void Draw(CrossHairSize size, Color color)
        {
            var colorBrush = new SolidColorBrush
            {
                Color = color
            };
            var line = new Line
            {
                Stroke = colorBrush,
                StrokeThickness = size.Height / 10d,
                X1 = 0,
                X2 = size.Width,
                Y1 = size.Height / 2d,
                Y2 = size.Height / 2d
            };


            this.Shapes.Add(line);

            var verticalLine = new Line
            {
                Stroke = colorBrush,
                StrokeThickness = size.Height / 10d,
                X1 = size.Width / 2d,
                X2 = size.Width / 2d,
                Y1 = 0,
                Y2 = size.Height
            };

            this.Shapes.Add(verticalLine);
        }
    }
}