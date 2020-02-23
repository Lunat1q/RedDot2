using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RD2.Crosshairs
{
    internal class Cross : BaseShapesContainer, ICrosshair
    {
        public Cross(List<Shape> shapes) : base(shapes)
        {
        }
        public bool AdjustOnlyLeft { get; } = false;

        public void Draw(CrossHairSize size, Color color)
        {
            var line = new Line();
            SolidColorBrush colorBrush = new SolidColorBrush
            {
                Color = color
            };
            line.Stroke = colorBrush;
            line.StrokeThickness = size.Height / 5d;

            line.X1 = 0;
            line.X2 = size.Width;
            line.Y1 = size.Height / 2d;
            line.Y2 = size.Height / 2d;

            this.Shapes.Add(line);

            var verticalLine = new Line();
            verticalLine.Stroke = colorBrush;
            verticalLine.StrokeThickness = size.Height / 5d;

            verticalLine.X1 = size.Width / 2d;
            verticalLine.X2 = size.Width / 2d;
            verticalLine.Y1 = 0;
            verticalLine.Y2 = size.Height;
            this.Shapes.Add(verticalLine);
        }
    }
}