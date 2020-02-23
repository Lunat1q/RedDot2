using System.Collections.Generic;
using System.Windows.Shapes;

namespace RD2.Crosshairs
{
    internal abstract class BaseShapesContainer
    {
        private protected List<Shape> Shapes;

        protected BaseShapesContainer(List<Shape> shapes)
        {
            this.Shapes = shapes;
        }
    }
}