using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using RD2.ViewModel;

namespace RD2.Crosshairs
{
    internal class Dot : BaseShapesContainer, ICrosshair
    {
        public Dot(List<Shape> shapes) : base(shapes)
        {
        }

        public Thickness GetPadding(CrossHairSize size) => new Thickness(0, 0, 0, 0);
        public void Draw(CrossHairSize size, Color color)
        {
            Ellipse dot = new Ellipse();
            SolidColorBrush colorBrush = new SolidColorBrush
            {
                Color = color
            };
            dot.Fill = colorBrush;
            dot.StrokeThickness = 0;

            // Set the width and height of the Ellipse.
            dot.Width = size.Width;
            dot.Height = size.Height;
            this.Shapes.Add(dot);
        }

        public bool AdjustOnlyLeft { get; } = false;
    }
}