using System.Windows.Media;

namespace RD2.Crosshairs
{
    internal interface ICrosshair
    {
        void Draw(CrossHairSize size, Color color);

        bool AdjustOnlyLeft { get; }
    }
}