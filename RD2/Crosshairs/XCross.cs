using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RD2.Crosshairs
{
    internal class XCross : BaseShapesContainer, ICrosshair
    {
        public XCross(List<Shape> shapes) : base(shapes)
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
            line.Y1 = 0;
            line.Y2 = size.Height;

            this.Shapes.Add(line);

            var verticalLine = new Line();
            verticalLine.Stroke = colorBrush;
            verticalLine.StrokeThickness = size.Height / 5d;

            verticalLine.X1 = 0;
            verticalLine.X2 = size.Width;
            verticalLine.Y1 = size.Height;
            verticalLine.Y2 = 0;
            this.Shapes.Add(verticalLine);
        }
    }
}