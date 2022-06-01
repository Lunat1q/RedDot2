using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using RD2.ViewModel;

namespace RD2.Crosshairs
{
    internal class XCross : BaseShapesContainer, ICrosshair
    {
        public XCross(List<Shape> shapes) : base(shapes)
        {
        }
        public bool AdjustOnlyLeft { get; } = false;

        public Thickness GetPadding(CrossHairSize size) => new Thickness(0, 0, 0, 0);
        public void Draw(CrossHairSize size, Color color)
        {
            var line = new Line();
            SolidColorBrush colorBrush = new SolidColorBrush
            {
                Color = color
            };
            line.Stroke = colorBrush;
            line.StrokeThickness = size.Height / 10d;

            line.X1 = 0;
            line.X2 = size.Width;
            line.Y1 = 0;
            line.Y2 = size.Height;

            this.Shapes.Add(line);

            var verticalLine = new Line
            {
                Stroke = colorBrush,
                StrokeThickness = size.Height / 10d,
                X1 = 0,
                X2 = size.Width,
                Y1 = size.Height,
                Y2 = 0
            };

            this.Shapes.Add(verticalLine);
        }
    }
}